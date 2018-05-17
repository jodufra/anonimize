using System;

namespace Anonimize.DataAccess
{
    public class EncryptedBooleanNullable : EncryptedBoolean
    {
        public override Type DefaultType => typeof(Boolean?);
    }
}
