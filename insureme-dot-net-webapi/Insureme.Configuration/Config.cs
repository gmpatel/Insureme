using System.Configuration;
using Insureme.Configuration.Elements;
using Insureme.Configuration.Interfaces;
using Insureme.Configuration.Sections;

namespace Insureme.Configuration
{
    public class Config : IConfig
    {
        private static long counter;

        private readonly ConfigSection configSection;

        public Config()
        {
            Id = ++counter;
            configSection = (ConfigSection) ConfigurationManager.GetSection("applicationConfiguration");
        }

        public long Id { get; private set; }

        public long Instances => counter;

        public TokenRelatedConfigurationElement TokenRelatedConfiguration => configSection.TokenRelatedConfiguration;
    }
}