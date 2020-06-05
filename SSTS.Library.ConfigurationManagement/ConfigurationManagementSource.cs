using SSTS.Library.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SSTS.Library.ConfigurationManagement
{
    public class ConfigurationManagementSource : IConfigurationManagementSource
    {
        private Dictionary<string, Tuple<DateTime, dynamic>> ConfigurationUnderManagement;

        private ConfigurationManagementSettings BaseSettings;

        public string DatabaseConnectionSetName { get { return "ConfigurationManagement"; } }

        public IDatabaseConnectionSet DatabaseConnectionSet { get; private set; }

        public IDatabaseReader DatabaseReader { get; private set; }

        public ConfigurationManagementSource(IEnumerable<IDatabaseConnectionSet> databaseConnectionSets, IDatabaseReader databaseReader)
        {
            this.DatabaseReader = databaseReader;

            if (!databaseConnectionSets.Any(dcs => this.DatabaseConnectionSetName.Equals(dcs.Name)))
            {
                throw new TypeLoadException(string.Format("Configuration missing for database connection named '{0}'", this.DatabaseConnectionSetName));
            }

            this.DatabaseConnectionSet = databaseConnectionSets.First<IDatabaseConnectionSet>(dcs => dcs.Name.Equals(this.DatabaseConnectionSetName));
        }

        public dynamic Load(string typeName)
        {
            if (this.ConfigurationUnderManagement == null)
            {
                this.ConfigurationUnderManagement = new Dictionary<string, Tuple<DateTime, dynamic>>();
            }

            if (this.BaseSettings == null)
            {
                var baseSettingsAsLoaded = this.DatabaseReader.Read(this.DatabaseConnectionSet.Connection, new Dictionary<string, object> { { "name", "SSTS.Base" } });

                this.BaseSettings = new ConfigurationManagementSettings(baseSettingsAsLoaded.configuration.maximumConfigurationAgeInMilliseconds);
            }

            if (!this.ConfigurationUnderManagement.ContainsKey(typeName) ||
                 this.ConfigurationUnderManagement[typeName].Item1 < DateTime.UtcNow.AddMilliseconds(this.BaseSettings.MaximumConfigurationAgeInMilliseconds * -1))
            {
                var document = this.DatabaseReader.Read(this.DatabaseConnectionSet.Connection, new Dictionary<string, object> { { "name", typeName } });

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
