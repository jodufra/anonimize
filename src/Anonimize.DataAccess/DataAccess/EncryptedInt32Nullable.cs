using System;

namespace Anonimize.DataAccess
{
    public class EncryptedInt32Nullable : EncryptedInt32
    {
        public override Type DefaultType => typeof(Int32?);
    }
}
