using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Insureme.Core.v1.Entities
{
    public class ConfigurationEntity
    {
        [JsonIgnore]
        //[JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonIgnore]
        public DateTime DateTimeCreated { get; set; }

        [JsonIgnore]
        public DateTime? DateTimeLastModified { get; set; }

        [JsonProperty(PropertyName = "backEndKey")]
        public string BackEndKey { get; set; }

        [JsonProperty(PropertyName = "tokenLifeSpanMinutes")]
        public double TokenLifeSpanMinutes { get; set; }

        [JsonProperty(PropertyName = "gstAustraliaPercent")]
        public float GstAustraliaPercent { get; set; }
    }
}