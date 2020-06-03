using System;

namespace SSTS.Library.Common.Connectivity
{
    public interface IDatabaseConnectionSet
    {
        string Name { get; }

        IDatabaseConnection Connection { get; }
    }
}
