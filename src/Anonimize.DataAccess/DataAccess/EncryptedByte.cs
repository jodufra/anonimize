using System;
using Telerik.OpenAccess.Data;

namespace Anonimize.DataAccess
{
    public class EncryptedByte : AEncryptedType
    {
        public override Type DefaultType => typeof(Byte);

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
                    holder.Int32Value = 0;
            }
            else
            {
                var encryptedValue = holder.Reader.GetValue(holder.Position).ToString();
                var decryptedValue = cryptoService.Decrypt<Byte>(encryptedValue);

                if (IsNullable || holder.Box)
                    holder.ObjectValue = decryptedValue;
                else
                    holder.Int32Value = decryptedValue;
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

            var decryptedValue = IsNullable && holder.ObjectValue == null ? (Byte?)null : (Byte)holder.Int32Value);
            var encryptedValue = cryptoService.Encrypt(decryptedValue);

            holder.Parameter.Size = encryptedValue.Length;
            holder.Parameter.Value = encryptedValue;
        }
    }
}
