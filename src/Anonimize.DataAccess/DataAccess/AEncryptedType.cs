using Anonimize.Services;
using System;
using Telerik.OpenAccess.Data;

namespace Anonimize.DataAccess
{
    public abstract class AEncryptedType : AdoTypeConverter
    {
        protected ICryptoService cryptoService;

        bool? isNullable;

        protected AEncryptedType()
        {
            cryptoService = AnonimizeProvider.GetInstance().GetCryptoService();
        }

        public override abstract Type DefaultType { get; }

        public bool IsNullable
        {
            get
            {
                if (!isNullable.HasValue)
                    isNullable = DefaultType.IsGenericType && DefaultType.GetGenericTypeDefinition() == typeof(Nullable<>);
                return isNullable.Value;
            }
        }

        public override bool CreateLiteralSql(ref DataHolder holder)
        {
            if (holder.NoValue)
                holder.StringValue = "NULL";

            return !holder.NoValue;
        }

        public override abstract object Read(ref DataHolder holder);

        public override abstract void Write(ref DataHolder holder);
    }
}
