using System;

namespace Anonimize.NHibernate.UserTypes
{
    [Serializable]
    public class EncryptedDateTime : EncryptedType<DateTime> { }
}
