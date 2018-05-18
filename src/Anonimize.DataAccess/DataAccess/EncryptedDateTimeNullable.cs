using System;

namespace Anonimize.DataAccess
{
    public class EncryptedDateTimeNullable : EncryptedDateTime
    {
        public override Type DefaultType => typeof(DateTime?);
    }
}
