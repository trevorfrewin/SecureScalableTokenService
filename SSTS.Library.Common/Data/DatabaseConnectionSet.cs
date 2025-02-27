namespace SSTS.Library.Common.Data
{
    public class DatabaseConnectionSet : IDatabaseConnectionSet
    {
        public required string Name { get; set; }

        public required IDatabaseConnection Connection { get; set; }
    }
}
