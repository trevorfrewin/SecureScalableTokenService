using System;

namespace SSTS.Library.Common.Data
{
    public class DatabaseConnection : IDatabaseConnection
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string CollectionName { get; set; }
    }
}
