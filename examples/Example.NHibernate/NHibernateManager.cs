using System.Linq;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using NHibernate;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode.Conformist;

namespace Example
{
    public static class NHibernateManager
    {
        const string SessionKey = "_SessionDatabaseKey";
        static ISession _Session;
        static ISessionFactory _SessionFactory;

        static ISessionFactory SessionFactory
        {
            get
            {
                if (_SessionFactory == null)
                {
                    var cfg = Setup();
                    _SessionFactory = cfg.BuildSessionFactory();
                }
                return _SessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            if (_Session == null || !_Session.IsOpen)
                _Session = CreateSession();
            return _Session;
        }

        static ISession CreateSession()
        {
            return SessionFactory.OpenSession();
        }

        static Configuration Setup()
        {
            //var serializer = new NHibernate.Mapping.Attributes.HbmSerializer { Validate = true };
            var cfg = new Configuration().DataBaseIntegration(db =>
            {
                db.ConnectionStringName = "Connection";
                db.Dialect<MySQL55Dialect>();
                db.Driver<MySqlDataDriver>();
                db.HqlToSqlSubstitutions = "true 1, false 0, yes 'Y', no 'N'";
                db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                db.SchemaAction = SchemaAutoAction.Validate;
            });

            var mapper = new ModelMapper();
            var mappedTypes = typeof(Mappings.UserMapping).Assembly.GetTypes().Where(x => x.Namespace == "Example.Mappings" && x.BaseType.IsGenericType && x.BaseType.GetGenericTypeDefinition() == typeof(ClassMapping<>));

            mapper.AddMappings(mappedTypes);
            cfg.AddDeserializedMapping(mapper.CompileMappingForAllExplicitlyAddedEntities(), null);
            return cfg;
        }
    }
}
