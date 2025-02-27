namespace SSTS.Library.Common.Data
{
    public class DatabaseConnection : IDatabaseConnection
    {
        public required string ConnectionString { get; set; }

        public required string DatabaseName { get; set; }

        public required string CollectionName { get; set; }
    }
}
