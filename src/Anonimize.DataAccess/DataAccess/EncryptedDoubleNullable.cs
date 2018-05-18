using System;

namespace Anonimize.DataAccess
{
    public class EncryptedDoubleNullable : EncryptedDouble
    {
        public override Type DefaultType => typeof(Double?);
    }
}
