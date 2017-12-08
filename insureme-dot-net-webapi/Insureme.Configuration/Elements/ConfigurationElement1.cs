using System.Configuration;

namespace Insureme.Configuration.Elements
{
    public class ConfigurationElement1 : ConfigurationElement
    {
        /*
        [ConfigurationProperty("currencyConfigurations")]
        [ConfigurationCollection(typeof(CurrencyConfigurationElementCollection), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
        public CurrencyConfigurationElementCollection CurrencyConfigurations
        {
            get
            {
                return (CurrencyConfigurationElementCollection)base["currencyConfigurations"];
            }
        }

        [ConfigurationProperty("instrumentGranularityMacdConfigurations")]
        [ConfigurationCollection(typeof(InstrumentGranularityMacdConfigurationElementCollection), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
        public InstrumentGranularityMacdConfigurationElementCollection InstrumentGranularityMacdConfigurations
        {
            get
            {
                return (InstrumentGranularityMacdConfigurationElementCollection)base["instrumentGranularityMacdConfigurations"];
            }
        }

        [ConfigurationProperty("oandaListenerConfiguration")]
        public OandaListenerConfigurationElement OandaListenerConfiguration
        {
            get { return ((OandaListenerConfigurationElement)(base["oandaListenerConfiguration"])); }
            set { base["oandaListenerConfiguration"] = value; }
        }

        [ConfigurationProperty("oandaTradingConfiguration")]
        public OandaTradingConfigurationElement OandaTradingConfiguration
        {
            get { return ((OandaTradingConfigurationElement)(base["oandaTradingConfiguration"])); }
            set { base["oandaTradingConfiguration"] = value; }
        }
        */
    }
}
