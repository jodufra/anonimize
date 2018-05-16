using System;

namespace Anonimize.NHibernate.UserTypes
{
    [Serializable]
    public class EncryptedString : EncryptedType<String> { }
}
