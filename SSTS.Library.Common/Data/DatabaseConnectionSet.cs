namespace SSTS.Library.Common.Data
{
    public class DatabaseConnectionSet : IDatabaseConnectionSet
    {
        public string Name { get; set; }

        public IDatabaseConnection Connection { get; set; }
    }
}
