namespace SSTS.Library.Common.Connectivity
{
    public class DatabaseConnectionSet : IDatabaseConnectionSet
    {
        public string Name { get; set; }

        public IDatabaseConnection Connection { get; set; }
    }
}
