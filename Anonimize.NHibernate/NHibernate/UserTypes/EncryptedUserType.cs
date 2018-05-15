using System;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using System.Data.Common;
using NHibernate;
using System.Data;

namespace Anonimize.NHibernate.UserTypes
{
    [Serializable]
    public class BaseEncryptedUserType<T> : AEncryptedUserType
    {
        public override bool IsMutable => false;

        public override Type ReturnedType => typeof(T);

        public override SqlType[] SqlTypes => new SqlType[] { new SqlType(DbType.String) };
       
        public override object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            if (names.Length == 0)
                throw new ArgumentException("Expecting at least one column");

            var encryptedValue = (string)NHibernateUtil.String.NullSafeGet(rs, names[0], session);

            var anonimize = AnonimizeProvider.GetInstance();
            var cryptoService = anonimize.GetCryptoService();

            var decryptedValue = cryptoService.Decrypt<T>(encryptedValue);

            return decryptedValue;
        }

        public override void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var parameter = cmd.Parameters[index];

            var anonimize = AnonimizeProvider.GetInstance();
            var cryptoService = anonimize.GetCryptoService();

            parameter.Value = cryptoService.Encrypt((T)value);
        }
    }
}
