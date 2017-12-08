using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Insureme.Core.v1.Entities
{
    public class LinkEntity
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [NotMapped]
        [JsonProperty(PropertyName = "type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string TypeString => Type?.Name;

        [JsonProperty(PropertyName = "typeId", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long TypeId { get; set; }

        //[JsonProperty(PropertyName = "product", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [NotMapped]
        [JsonIgnore]
        public string ProductString => Product?.Title;

        //[JsonProperty(PropertyName = "productId", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonIgnore]
        public long ProductId { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "hint")]
        public string Hint { get; set; }

        [JsonIgnore]
        public bool Enabled { get; set; }

        [JsonIgnore]
        public DateTime DateTimeCreated { get; set; }

        [JsonIgnore]
        public DateTime? DateTimeLastModified { get; set; }

        [JsonIgnore]
        public virtual LinkTypeEntity Type { get; set; }

        [JsonIgnore]
        public virtual ProductEntity Product { get; set; }
    }
}