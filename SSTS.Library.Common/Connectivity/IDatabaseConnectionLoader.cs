using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace SSTS.Library.Common.Connectivity
{
    public interface IDatabaseConnectionLoader
    {
        public IEnumerable<IDatabaseConnectionSet> FromAppSettings(IConfigurationSection baseSection);
    }
}
