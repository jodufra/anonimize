using System;

namespace Anonimize.DataAccess
{
    public class EncryptedDecimalNullable : EncryptedDecimal
    {
        public override Type DefaultType => typeof(Decimal?);
    }
}
