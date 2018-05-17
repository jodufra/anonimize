using System;
using NHibernate.Engine;
using System.Data.Common;
using NHibernate;

namespace Anonimize.NHibernate
{
    [Serializable]
    public class EncryptedStringAnalogous : EncryptedType<string>
    {
        public override object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            if (names.Length == 0)
                throw new ArgumentException("Expecting at least one column");

            var encryptedValue = (string)NHibernateUtil.String.NullSafeGet(rs, names[0], session);

            var decryptedValue = cryptoService.Decrypt<string>(encryptedValue);
            if (string.IsNullOrEmpty(decryptedValue))
                decryptedValue = encryptedValue;

            return decryptedValue;
        }

        public override void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var parameter = cmd.Parameters[index];

            var decryptedValue = cryptoService.Decrypt<string>((string)value);
            if (string.IsNullOrEmpty(decryptedValue))
            {
                // the decrypt function failed or the original value is empty 
                // the value is not encrypted
                parameter.Value = cryptoService.Encrypt((string)value);
            }
            else
            {
                // the value is already encrypted
                parameter.Value = value;
            }
        }
    }
}
