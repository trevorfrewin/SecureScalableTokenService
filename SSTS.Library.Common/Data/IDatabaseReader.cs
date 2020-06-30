using System.Collections.Generic;

namespace SSTS.Library.Common.Data
{
    public interface IDatabaseReader
    {
        dynamic Read(Dictionary<string, object> filter);
    }
}
