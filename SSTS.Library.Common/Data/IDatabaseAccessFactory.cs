namespace SSTS.Library.Common.Data
{
    public interface IDatabaseAccessFactory
    {
        IDatabaseReader GetReader(string connectionSetName);

        IDatabaseWriter GetWriter(string connectionSetName);

    }
}
