using System;
using Telerik.OpenAccess.Data;

namespace Anonimize.DataAccess
{
    public class EncryptedStringAnalogous : AEncryptedType
    {
        public override Type DefaultType => typeof(String);

        public override object Read(ref DataHolder holder)
        {
            holder.NoValue = holder.Reader.IsDBNull(holder.Position);

            if(holder.NoValue)
            {
                holder.StringValue = String.Empty;
            }
            else
            {
                var encryptedValue = holder.Reader.GetValue(holder.Position).ToString();
                var decryptedValue = cryptoService.Decrypt<String>(encryptedValue);

                if (decryptedValue == null)
                    decryptedValue = encryptedValue;

                holder.StringValue = decryptedValue;
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

            var value = holder.StringValue;

            var decryptedValue = cryptoService.Decrypt<string>(value);
            var encryptedValue = decryptedValue == null ? cryptoService.Encrypt(decryptedValue) : value;

            holder.Parameter.Size = encryptedValue.Length;
            holder.Parameter.Value = encryptedValue;
        }
    }
}
