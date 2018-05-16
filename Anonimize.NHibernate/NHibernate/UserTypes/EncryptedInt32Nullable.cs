using System;

namespace Anonimize.NHibernate.UserTypes
{
    [Serializable]
    public class EncryptedInt32Nullable : EncryptedType<Int32?> { }
}
