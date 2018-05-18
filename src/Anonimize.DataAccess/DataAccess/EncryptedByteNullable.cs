using System;

namespace Anonimize.DataAccess
{
    public class EncryptedByteNullable : EncryptedByte
    {
        public override Type DefaultType => typeof(Byte?);
    }
}
