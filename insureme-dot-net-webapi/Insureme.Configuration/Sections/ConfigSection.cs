using System.Configuration;
using Insureme.Configuration.Elements;

namespace Insureme.Configuration.Sections
{
    public class ConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("tokenRelatedConfiguration")]
        public TokenRelatedConfigurationElement TokenRelatedConfiguration
        {
            get { return ((TokenRelatedConfigurationElement)(base["tokenRelatedConfiguration"])); }
            set { base["tokenRelatedConfiguration"] = value; }
        }

        /*
        [ConfigurationProperty("greenForex")]
        public ConfigurationElement1 GreenForex
        {
            get { return ((ConfigurationElement1)(base["greenForex"])); }
            set { base["greenForex"] = value; }
        }
        */
    }
}                    