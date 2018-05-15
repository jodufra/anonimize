using System;

namespace Anonimize.NHibernate.UserTypes
{
    [Serializable]
    public class EncryptedDateTimeUserType : BaseEncryptedUserType<DateTime> { }

    [Serializable]
    public class EncryptedDateTimeNullableUserType : BaseEncryptedUserType<DateTime?> { }

    [Serializable]
    public class EncryptedInt32UserType : BaseEncryptedUserType<Int32> { }

    [Serializable]
    public class EncryptedInt32NullableUserType : BaseEncryptedUserType<Int32?> { }

    [Serializable]
    public class EncryptedDecimalUserType : BaseEncryptedUserType<Decimal> { }

    [Serializable]
    public class EncryptedDecimalNullableUserType : BaseEncryptedUserType<Decimal?> { }

    [Serializable]
    public class EncryptedStringUserType : BaseEncryptedUserType<String> { }
}
