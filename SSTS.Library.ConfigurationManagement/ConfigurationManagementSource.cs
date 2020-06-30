using SSTS.Library.Common.Data;
using System;
using System.Collections.Generic;

namespace SSTS.Library.ConfigurationManagement
{
    public class ConfigurationManagementSource : IConfigurationManagementSource
    {
        private Dictionary<string, Tuple<DateTime, dynamic>> ConfigurationUnderManagement;

        private ConfigurationManagementSettings BaseSettings;

        public string DatabaseConnectionSetName { get { return "ConfigurationManagement"; } }

        public IDatabaseAccessFactory DatabaseAccessFactory { get; private set; }

        public ConfigurationManagementSource(IDatabaseAccessFactory databaseAccessFactory)
        {
            this.DatabaseAccessFactory = databaseAccessFactory;
        }

        public dynamic Load(string typeName)
        {
            if (this.ConfigurationUnderManagement == null)
            {
                this.ConfigurationUnderManagement = new Dictionary<string, Tuple<DateTime, dynamic>>();
            }

            if (this.BaseSettings == null)
            {
                var databaseReader = this.DatabaseAccessFactory.GetReader("ConfigurationManagement");
                var baseSettingsAsLoaded = databaseReader.Read(new Dictionary<string, object> { { "name", "SSTS.Base" } });

                this.BaseSettings = new ConfigurationManagementSettings(baseSettingsAsLoaded.configuration.maximumConfigurationAgeInMilliseconds);
            }

            if (!this.ConfigurationUnderManagement.ContainsKey(typeName) ||
                 this.ConfigurationUnderManagement[typeName].Item1 < DateTime.UtcNow.AddMilliseconds(this.BaseSettings.MaximumConfigurationAgeInMilliseconds * -1))
            {
                var databaseReader = this.DatabaseAccessFactory.GetReader("ConfigurationManagement");
                var document = databaseReader.Read(new Dictionary<string, object> { { "name", typeName } });

                if (document == null)
                {
                    throw new ArgumentException(string.Format("No Configuration for '{0}' found", typeName));
                }

                if (this.ConfigurationUnderManagement.ContainsKey(typeName))
                {
                    this.ConfigurationUnderManagement.Remove(typeName);
                }

                this.ConfigurationUnderManagement.Add(typeName, new Tuple<DateTime, dynamic>(DateTime.UtcNow, document));

                return document;
            }

            return this.ConfigurationUnderManagement[typeName].Item2;
        }
    }
}
