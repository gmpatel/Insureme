using System.Configuration;

namespace Insureme.Configuration.Elements
{
    public class TokenRelatedConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("tokenValidityMinutes", DefaultValue = 30.00, IsRequired = true)]
        public double TokenValidityMinutes
        {
            get { return (double) base["tokenValidityMinutes"]; }
            set { base["tokenValidityMinutes"] = value; }
        }
    }
}