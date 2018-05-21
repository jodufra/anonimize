//---------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the Telerik.DataAccess.Fluent.CodeGeneration GenerateDataLayer.ps1 script
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated. 
//     To extend this class please create an additional partial class definition and put your code there.
// </auto-generated>
//---------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;

namespace ApplicationLib.Entities
{
    public partial class ConnectionEntitiesModel : OpenAccessContext
    {
        private static string connectionStringName = @"Connection";
            
        private static BackendConfiguration backend = GetBackendConfiguration();
                
        private static MetadataContainer metadataContainer = new ConnectionMetadataSource().GetModel();
        
        public ConnectionEntitiesModel()
            : base(connectionStringName, backend, metadataContainer)
        { }
        
        public ConnectionEntitiesModel(string connection)
            : base(connection, backend, metadataContainer)
        { }
        
        public ConnectionEntitiesModel(BackendConfiguration backendConfiguration)
            : base(connectionStringName, backendConfiguration, metadataContainer)
        { }
            
        public ConnectionEntitiesModel(string connection, MetadataContainer metadataContainer)
            : base(connection, backend, metadataContainer)
        { }
        
        public ConnectionEntitiesModel(string connection, BackendConfiguration backendConfiguration, MetadataContainer metadataContainer)
            : base(connection, backendConfiguration, metadataContainer)
        { }
        
        public static BackendConfiguration GetBackendConfiguration()
        {
            BackendConfiguration backend = new BackendConfiguration();
            backend.Backend = "MySQL";
            backend.ProviderName = "MySql.Data.MySqlClient";

            backend.Logging.LogEvents = LoggingLevel.All;
        
            CustomizeBackendConfiguration(ref backend);
        
            return backend;
        }
        
        /// <summary>
        /// Allows you to customize the BackendConfiguration of FluentModel.
        /// </summary>
        /// <param name="config">The BackendConfiguration of FluentModel.</param>
        static partial void CustomizeBackendConfiguration(ref BackendConfiguration config);

        public IQueryable<User> Users 
        { 
            get { return this.GetAll<User>(); }
        }

    }
}
