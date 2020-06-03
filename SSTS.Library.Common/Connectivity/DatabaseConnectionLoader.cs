using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace SSTS.Library.Common.Connectivity
{
    public class DatabaseConnectionLoader : IDatabaseConnectionLoader
    {
        public IConfiguration Configuration { get; private set; }
        
        public IEnumerable<IDatabaseConnectionSet> FromAppSettings(IConfigurationSection baseSection)
        {
            var databaseConnectionSets = new List<IDatabaseConnectionSet>();

            foreach (var databaseConnectionConfigurationChild in baseSection.GetChildren())
            {
                databaseConnectionSets.Add(new DatabaseConnectionSet
                {
                    Name = databaseConnectionConfigurationChild.Key,
                    Connection = new DatabaseConnection
                    {
                        ConnectionString = baseSection[string.Format("{0}:{1}", databaseConnectionConfigurationChild.Key, "ConnectionString")],
                        DatabaseName = baseSection[string.Format("{0}:{1}", databaseConnectionConfigurationChild.Key, "DatabaseName")],
                        CollectionName = baseSection[string.Format("{0}:{1}", databaseConnectionConfigurationChild.Key, "CollectionName")]
                    }
                }
                );
            }

            return databaseConnectionSets;
        }
    }
}