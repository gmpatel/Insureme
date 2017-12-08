using Insureme.Configuration.Elements;

namespace Insureme.Configuration.Interfaces
{
    public interface IConfig
    {
        long Id { get; }

        long Instances { get; }

        TokenRelatedConfigurationElement TokenRelatedConfiguration { get; }
    }
}