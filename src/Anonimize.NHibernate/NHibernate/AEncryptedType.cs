using NHibernate;
using System;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using System.Data.Common;
using Anonimize.Services;
using NHibernate.UserTypes;
using System.Data;

namespace Anonimize.NHibernate
{
    public abstract class AEncryptedType<T> : IUserType
    {
        protected ICryptoService cryptoService;

        protected AEncryptedType()
        {
            cryptoService = AnonimizeProvider.GetInstance().GetCryptoService();
        }

        public bool IsMutable => false;

        public Type ReturnedType => typeof(T);

        public SqlType[] SqlTypes => new SqlType[] { new SqlType(DbType.String) };

        public object Assemble(object cached, object owner)
        {
            return cached;
        }
        
        public object DeepCopy(object value)
        {
            return value;
        }

        public object Disassemble(object value)
        {
            return value;
        }
        
        public new bool Equals(object x, object y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x == null || y == null)
                return false;

            return x.Equals(y);
        }

        public int GetHashCode(object x)
        {
            if (x == null)
                return 0;

            return x.GetHashCode();
        }

        public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            if (names.Length == 0)
                throw new ArgumentException("Expecting at least one column");

            var encryptedValue = (string)NHibernateUtil.String.NullSafeGet(rs, names[0], session);            
            var decryptedValue = cryptoService.Decrypt<T>(encryptedValue);

            return decryptedValue;
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var parameter = cmd.Parameters[index];

            parameter.Value = cryptoService.Encrypt((T)value);
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }
    }
}
