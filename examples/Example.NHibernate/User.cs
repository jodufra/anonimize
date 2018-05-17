using Anonimize.NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Example
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
    }
}

namespace Example.Mappings
{
    public class UserMapping : ClassMapping<User>
    {
        public UserMapping()
        {
            Table("user");
            Lazy(true);

            Id(x => x.Id, x => { x.Column("Id"); x.Generator(Generators.Identity); });

            Property(x => x.Name, x => { x.Column("Name"); x.NotNullable(true); x.Type<EncryptedString>(); });
            Property(x => x.Email, x => { x.Column("Email"); x.NotNullable(true); x.Type<EncryptedStringAnalogous>(); });
        }
    }
}