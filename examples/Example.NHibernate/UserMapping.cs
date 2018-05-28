using Anonimize.NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Example.Mappings
{
    public class UserMapping : ClassMapping<User>
    {
        public UserMapping()
        {
            Table("user");
            Lazy(true);

            Id(x => x.Id, x => { x.Column("Id"); x.Generator(Generators.Identity); });

            Property(x => x.AccountBalance, x => { x.Column("AccountBalance"); x.NotNullable(true); x.Type<EncryptedDecimal>(); });
            Property(x => x.AccountDebt, x => { x.Column("AccountDebt"); x.NotNullable(false); x.Type<EncryptedDecimalNullable>(); });
            Property(x => x.Address, x => { x.Column("Address"); x.NotNullable(false); x.Type<EncryptedString>(); });
            Property(x => x.CivilId, x => { x.Column("CivilId"); x.NotNullable(true); x.Type<EncryptedInt32>(); });
            Property(x => x.DateCreated, x => { x.Column("DateCreated"); x.NotNullable(true); x.Type<EncryptedDateTime>(); });
            Property(x => x.DateUpdated, x => { x.Column("DateUpdated"); x.NotNullable(false); x.Type<EncryptedDateTimeNullable>(); });
            Property(x => x.Email, x => { x.Column("Email"); x.NotNullable(true); x.Type<EncryptedStringAnalogous>(); });
            Property(x => x.FiscalId, x => { x.Column("FiscalId"); x.NotNullable(false); x.Type<EncryptedInt32Nullable>(); });
            Property(x => x.IsActive, x => { x.Column("IsActive"); x.NotNullable(true); x.Type<EncryptedBoolean>(); });
            Property(x => x.IsFemale, x => { x.Column("IsFemale"); x.NotNullable(false); x.Type<EncryptedBooleanNullable>(); });
            Property(x => x.Name, x => { x.Column("Name"); x.NotNullable(true); x.Type<EncryptedString>(); });
        }
    }
}