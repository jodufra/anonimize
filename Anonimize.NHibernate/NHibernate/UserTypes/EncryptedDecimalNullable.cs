using System;

namespace Anonimize.NHibernate.UserTypes
{
    [Serializable]
    public class EncryptedDecimalNullable : EncryptedType<Decimal?> { }
}
