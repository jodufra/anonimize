using System;

namespace Anonimize.NHibernate.UserTypes
{
    [Serializable]
    public class EncryptedDoubleNullable : EncryptedType<Double?> { }
}
