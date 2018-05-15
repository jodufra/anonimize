using Anonimize.NHibernate.UserTypes;
using NHibernate.Mapping.ByCode.Conformist;

namespace Example.NHibernate
{
    public class User
    {
        public virtual string Name { get; set; }

        public virtual string Email { get; set; }
    }

    public class UserMapping : ClassMapping<User>
    {
        public UserMapping()
        {
            this.Table("user");
            this.Lazy(true);

            this.Property(x => x.Name, x => { x.Column("Name"); x.NotNullable(true); x.Type<EncryptedStringUserType>(); });
            this.Property(x => x.Email, x => { x.Column("Email"); x.NotNullable(true); x.Type<EncryptedStringUserType>(); });
        }
    }
}
