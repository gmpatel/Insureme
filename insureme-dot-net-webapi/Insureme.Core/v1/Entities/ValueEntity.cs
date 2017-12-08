using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Insureme.Core.v1.Entities
{
    public class ValueEntity
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "productId")]
        public long ProductId { get; set; }

        [JsonProperty(PropertyName = "propertyId")]
        public long PropertyId { get; set; }

        [NotMapped]
        [JsonProperty(PropertyName = "type")]
        public string TypeString => Type.Name;

        [JsonProperty(PropertyName = "typeId")]
        public long TypeId { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "unit")]
        public string Unit { get; set; }

        [JsonProperty(PropertyName = "comments")]
        public string Comments { get; set; }

        [JsonIgnore]
        public DateTime DateTimeCreated { get; set; }

        [JsonIgnore]
        public DateTime? DateTimeLastModified { get; set; }

        [JsonIgnore]
        public virtual ValueTypeEntity Type { get; set; }
    }
}