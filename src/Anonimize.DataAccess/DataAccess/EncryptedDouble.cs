using System;
using Telerik.OpenAccess.Data;

namespace Anonimize.DataAccess
{
    public class EncryptedDouble : AEncryptedType
    {
        public override Type DefaultType => typeof(Double);

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
                    holder.DoubleValue = 0;
            }
            else
            {
                var encryptedValue = holder.Reader.GetValue(holder.Position).ToString();
                var decryptedValue = cryptoService.Decrypt<Double>(encryptedValue);

                if (IsNullable || holder.Box)
                    holder.ObjectValue = decryptedValue;
                else
                    holder.DoubleValue = decryptedValue;
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

            var decryptedValue = IsNullable && holder.ObjectValue == null ? (Double?)null : holder.DoubleValue;
            var encryptedValue = cryptoService.Encrypt(decryptedValue);

            holder.Parameter.Size = encryptedValue.Length;
            holder.Parameter.Value = encryptedValue;
        }
    }
}
