using System;

namespace SSTS.Library.Common.Connectivity
{
    public interface IDatabaseConnection
    {
        string ConnectionString { get; }

        string DatabaseName { get; }

        string CollectionName { get; }
    }
}
