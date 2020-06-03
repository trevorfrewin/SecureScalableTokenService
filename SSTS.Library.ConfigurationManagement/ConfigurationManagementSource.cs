using MongoDB.Driver;
using SSTS.Library.Common.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;

namespace SSTS.Library.ConfigurationManagement
{
    public class ConfigurationManagementSource : IConfigurationManagementSource
    {
        private Dictionary<string, dynamic> ConfigurationUnderManagement;

        public string DatabaseConnectionSetName { get { return "ConfigurationManagement"; } }

        public IEnumerable<IDatabaseConnectionSet> DatabaseConnectionSets { get; private set; }

        public ConfigurationManagementSource(IEnumerable<IDatabaseConnectionSet> databaseConnectionSets)
        {
            this.DatabaseConnectionSets = databaseConnectionSets;

            if(!databaseConnectionSets.Any(dcs => this.DatabaseConnectionSetName.Equals(dcs.Name)))
            {
                throw new TypeLoadException(string.Format("Configuration missing for database connection named '{0}'", this.DatabaseConnectionSetName));
            }

            var databaseConnectionSet = this.DatabaseConnectionSets.First<IDatabaseConnectionSet>(dcs => dcs.Name.Equals(this.DatabaseConnectionSetName));
            var client = new MongoClient(databaseConnectionSet.Connection.ConnectionString);
            var database = client.GetDatabase(databaseConnectionSet.Connection.DatabaseName);
            var collection = database.GetCollection<dynamic>(databaseConnectionSet.Connection.CollectionName);

            var configurationManagement = new Dictionary<string, dynamic>();

            foreach(var documentFromCollection in collection.AsQueryable())
            {
                configurationManagement.Add(documentFromCollection.name, documentFromCollection.configuration);
            }

            this.ConfigurationUnderManagement = configurationManagement;
        }

        public dynamic Load(string typeName)
        {
            if (!this.ConfigurationUnderManagement.ContainsKey(typeName))
            {
                throw new ArgumentException(string.Format("No Configuration for '{0}' found", typeName));
            }

            return this.ConfigurationUnderManagement[typeName];
        }
    }
}
