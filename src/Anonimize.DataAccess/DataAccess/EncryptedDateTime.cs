using System;
using Telerik.OpenAccess.Data;

namespace Anonimize.DataAccess
{
    public class EncryptedDateTime : AEncryptedType
    {
        public override Type DefaultType => typeof(DateTime);

        public override object Read(ref DataHolder holder)
        {
            holder.NoValue = holder.Reader.IsDBNull(holder.Position);

            if (holder.NoValue)
            {
                if (IsNullable)
                    holder.ObjectValue = null;
                else if (holder.Box)
                    holder.ObjectValue = 0;
                else
                    holder.DateTimeValue = DateTime.MinValue;
            }
            else
            {
                var encryptedValue = holder.Reader.GetValue(holder.Position).ToString();
                var decryptedValue = cryptoService.Decrypt<DateTime>(encryptedValue);

                if (IsNullable || holder.Box)
                    holder.ObjectValue = decryptedValue;
                else
                    holder.DateTimeValue = decryptedValue;
            }

            return holder.ObjectValue;
        }

        public override void Write(ref DataHolder holder)
        {
            holder.Parameter.DbType = System.Data.DbType.String;

            if (holder.NoValue)
            {
                holder.Parameter.Value = null;
                return;
            }

            var decryptedValue = IsNullable && holder.ObjectValue == null ? (DateTime?)null : holder.DateTimeValue;
            var encryptedValue = cryptoService.Encrypt(decryptedValue);

            holder.Parameter.Size = encryptedValue.Length;
            holder.Parameter.Value = encryptedValue;
        }
    }
}
