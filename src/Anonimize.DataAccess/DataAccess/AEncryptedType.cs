using Anonimize.Services;
using System;
using System.Collections.Generic;
using Telerik.OpenAccess.Data;

namespace Anonimize.DataAccess
{
    public abstract class AEncryptedType<T> : AdoTypeConverter
    {
        protected ICryptoService cryptoService;

        bool? isNullable;

        protected AEncryptedType()
        {
            cryptoService = AnonimizeProvider.GetInstance().GetCryptoService();
        }

        public override Type DefaultType => typeof(T);

        public bool IsNullable
        {
            get
            {
                if (!isNullable.HasValue)
                    isNullable = DefaultType.IsGenericType && DefaultType.GetGenericTypeDefinition() == typeof(Nullable<>);
                return isNullable.Value;
            }
        }

        public bool IsString => DefaultType == typeof(string);

        public override bool CreateLiteralSql(ref DataHolder holder)
        {
            if (holder.NoValue)
                holder.StringValue = "NULL";
            else
                holder.StringValue = holder.ObjectValue.ToString();

            return !holder.NoValue;
        }

        public override AdoTypeConverter Initialize(IDataColumn user, Type clr, IAdoTypeConverterRegistry registry, bool secondaryTable)
        {
            if (clr != DefaultType)
                throw new ArgumentOutOfRangeException(nameof(clr), $"{GetType().FullName} converter is not compatible with properties of type {clr.Name}.");

            return base.Initialize(user, clr, registry, secondaryTable);
        }

        public override object Read(ref DataHolder holder)
        {
            holder.NoValue = holder.Reader.IsDBNull(holder.Position);

            if (holder.NoValue)
            {
                if (IsNullable)
                    return null;

                return default(T);
            }

            var encryptedValue = holder.Reader.GetValue(holder.Position);

            if (holder.Reader.GetType().FullName == "Telerik.OpenAccess.RT.Adonet2Generic.Impl.BufferingReader")
                return encryptedValue;

            var decryptedValue = cryptoService.Decrypt<T>(encryptedValue.ToString());

            if (DefaultType == typeof(string) && decryptedValue == null)
            {
                return encryptedValue;
            }

            return decryptedValue;
        }

        public override void Write(ref DataHolder holder)
        {
            holder.Parameter.DbType = System.Data.DbType.String;

            if (holder.NoValue)
            {
                holder.Parameter.Value = null;
                return;
            }

            var decryptedValue = GetHolderValue(ref holder);
            string encryptedValue;

            if (IsString)
            {
                var previousEncryptedValue = cryptoService.Decrypt<T>(decryptedValue as string);
                encryptedValue = EqualityComparer<T>.Default.Equals(previousEncryptedValue, default(T)) ? cryptoService.Encrypt(decryptedValue) : previousEncryptedValue as string;
            }
            else
            {
                encryptedValue = cryptoService.Encrypt(decryptedValue);
            }

            holder.Parameter.Value = encryptedValue;

            if (holder.Parameter.Value != null)
            {
                holder.Parameter.Size = encryptedValue.Length;
            }
        }

        protected virtual T GetHolderValue(ref DataHolder holder)
        {
            return IsNullable && holder.ObjectValue == null ? default(T) : (T)holder.ObjectValue;
        }
    }
}
