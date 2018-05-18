using System;

namespace Anonimize.DataAccess
{
    public class EncryptedSingleNullable : EncryptedSingle
    {
        public override Type DefaultType => typeof(Single?);
    }
}
