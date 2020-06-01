using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SSTS.Library.ConfigurationManagement
{
    public class ConfigurationManagementSource : IConfigurationManagementSource
    {
        private Dictionary<string, dynamic> ConfigurationUnderManagement;

        public ConfigurationManagementSource()
        {
            var client = new MongoClient("mongodb+srv://SSTSBaseUser:<password>@sstsmongocluster-9mtfs.azure.mongodb.net/test?retryWrites=true&w=majority");
            var database = client.GetDatabase("ConfigurationManagement");

            var collection = database.GetCollection<dynamic>("Configuration");

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
                throw new ArgumentException(string.Format("Configuration for '{0}' not found", typeName));
            }

            return this.ConfigurationUnderManagement[typeName];
        }
    }
}
