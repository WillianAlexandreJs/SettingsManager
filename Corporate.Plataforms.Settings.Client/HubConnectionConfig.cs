using System;
using System.Configuration;

namespace Corporate.Plataforms.Settings.Client
{
    public class HubConnectionConfig : ConfigurationSection
    {
        private static readonly HubConnectionConfig settings = ConfigurationManager.GetSection("HubConnectionConfig") as HubConnectionConfig;

        public static HubConnectionConfig Settings
        {
            get
            {
                return settings;
            }
        }

        [ConfigurationProperty("hubConnectionUrl", IsRequired = true)]
        public string HubConnectionUrl
        {
            get { return (string)this["hubConnectionUrl"]; }
            set { this["hubConnectionUrl"] = value; }
        }

        [ConfigurationProperty("hubProxyName", IsRequired = true)]
        public string HubProxyName
        {
            get { return (string)this["hubProxyName"]; }
            set { this["hubProxyName"] = value; }
        }

        [ConfigurationProperty("applicationName", IsRequired = true)]
        public string ApplicationName
        {
            get { return (string)this["applicationName"]; }
            set { this["applicationName"] = value; }
        }

        [ConfigurationProperty("applicationId", IsRequired = true)]
        public string ApplicationId
        {
            get { return (string)this["applicationId"]; }
            set { this["applicationId"] = value; }
        }

        [ConfigurationProperty("KeepAliveInterval", DefaultValue = "00:00:05", IsRequired = true)]
        public TimeSpan KeepAliveInterval
        {
            get { return (TimeSpan)this["KeepAliveInterval"]; }
            set { this["KeepAliveInterval"] = value; }
        }
    }
}
