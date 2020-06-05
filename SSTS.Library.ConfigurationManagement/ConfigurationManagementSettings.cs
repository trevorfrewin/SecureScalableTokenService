namespace SSTS.Library.ConfigurationManagement
{
    public class ConfigurationManagementSettings
    {
        public int MaximumConfigurationAgeInMilliseconds { get; private set; }

        public ConfigurationManagementSettings(int maximumConfigurationAgeInMilliseconds)
        {
            this.MaximumConfigurationAgeInMilliseconds = maximumConfigurationAgeInMilliseconds;
        }
    }
}