using System;
using Telerik.OpenAccess.Data;

namespace Anonimize.DataAccess
{
    public class EncryptedSingle : AEncryptedType
    {
        public override Type DefaultType => typeof(Single);

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
                    holder.SingleValue = 0;
            }
            else
            {
                var encryptedValue = holder.Reader.GetValue(holder.Position).ToString();
                var decryptedValue = cryptoService.Decrypt<Single>(encryptedValue);

                if (IsNullable || holder.Box)
                    holder.ObjectValue = decryptedValue;
                else
                    holder.SingleValue = decryptedValue;
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

            var decryptedValue = IsNullable && holder.ObjectValue == null ? (Single?)null : holder.SingleValue;
            var encryptedValue = cryptoService.Encrypt(decryptedValue);

            holder.Parameter.Size = encryptedValue.Length;
            holder.Parameter.Value = encryptedValue;
        }
    }
}
