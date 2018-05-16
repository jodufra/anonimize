using System;

namespace Anonimize.NHibernate.UserTypes
{
    [Serializable]
    public class EncryptedDateTimeNullable : EncryptedType<DateTime?> { }
}
