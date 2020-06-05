using System;

namespace SSTS.Library.Common.Data
{
    public interface IDatabaseConnection
    {
        string ConnectionString { get; }

        string DatabaseName { get; }

        string CollectionName { get; }
    }
}
