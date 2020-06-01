using System;

namespace SSTS.Library.ConfigurationManagement
{
    public interface IConfigurationManagementSource
    {
        dynamic Load(string typeName);
    }
}
