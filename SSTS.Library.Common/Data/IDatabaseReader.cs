using System.Collections.Generic;

namespace SSTS.Library.Common.Data
{
    public interface IDatabaseReader
    {
        dynamic Read(IDatabaseConnection connection, Dictionary<string, object> filter);
    }
}
