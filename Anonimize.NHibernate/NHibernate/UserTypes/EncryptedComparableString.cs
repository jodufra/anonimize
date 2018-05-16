using System;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using System.Data.Common;
using NHibernate;
using System.Data;

namespace Anonimize.NHibernate.UserTypes
{
    [Serializable]
    public class EncryptedComparableString : EncryptedType<string>
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
                // the original value is empty or the decrypt function failed, that means
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
