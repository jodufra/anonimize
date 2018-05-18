using System;
using NHibernate.Engine;
using System.Data.Common;
using NHibernate;

namespace Anonimize.NHibernate
{
    [Serializable]
    public class EncryptedStringAnalogous : AEncryptedType<string>
    {
        public new object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            if (names.Length == 0)
                throw new ArgumentException("Expecting at least one column");

            var encryptedValue = (string)NHibernateUtil.String.NullSafeGet(rs, names[0], session);

            var decryptedValue = cryptoService.Decrypt<string>(encryptedValue);

            if (decryptedValue == null)
                decryptedValue = encryptedValue;

            return decryptedValue;
        }

        public new void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var parameter = cmd.Parameters[index];

            var decryptedValue = cryptoService.Decrypt<string>((string)value);
            var encryptedValue = decryptedValue == null ? cryptoService.Encrypt((string)value) : value;

            parameter.Value = encryptedValue;
        }
    }
}
