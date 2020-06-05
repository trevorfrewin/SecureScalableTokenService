namespace SSTS.Library.Common.Data
{
    public interface IDatabaseConnectionSet
    {
        string Name { get; }

        IDatabaseConnection Connection { get; }
    }
}
