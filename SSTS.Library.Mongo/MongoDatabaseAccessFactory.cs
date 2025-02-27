using SSTS.Library.Common.Data;

namespace SSTS.Library.Mongo
{
    public class MongoDatabaseAccessFactory : IDatabaseAccessFactory
    {
        public IEnumerable<IDatabaseConnectionSet> DatabaseConnectionSets { get; private set; }

        public MongoDatabaseAccessFactory(IEnumerable<IDatabaseConnectionSet> databaseConnectionSets)
        {
            this.DatabaseConnectionSets = databaseConnectionSets;
        }

        public IDatabaseReader GetReader(string connectionSetName)
        {
            return new DatabaseReader(this.GetNamedConnectionSet(connectionSetName).Connection);
        }

        public IDatabaseWriter GetWriter(string connectionSetName)
        {
            return new DatabaseWriter(this.GetNamedConnectionSet(connectionSetName).Connection);
        }

        private IDatabaseConnectionSet GetNamedConnectionSet(string connectionSetName)
        {
            if (!this.DatabaseConnectionSets.Any(dcs => dcs.Name.Equals(connectionSetName)))
            {
                throw new ArgumentException(string.Format("No database connection set found in configuration with the name: '{0}'.", connectionSetName));
            }

            return this.DatabaseConnectionSets.First(dcs => dcs.Name.Equals(connectionSetName));
        }
    }
}
