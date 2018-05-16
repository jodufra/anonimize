using System;

namespace Anonimize.NHibernate.UserTypes
{
    [Serializable]
    public class EncryptedDecimal : EncryptedType<Decimal> { }
}
