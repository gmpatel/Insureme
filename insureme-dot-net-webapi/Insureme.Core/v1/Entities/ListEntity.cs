using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Insureme.Core.v1.Entities
{
    public class ListEntity
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonIgnore]
        public bool Enabled { get; set; }

        [JsonIgnore]
        public DateTime DateTimeCreated { get; set; }

        [JsonIgnore]
        public DateTime? DateTimeLastModified { get; set; }

        [JsonProperty(PropertyName = "listItems", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual ICollection<ListItemEntity> ListItems { get; set; }

        [JsonIgnore]
        public virtual ICollection<PropertyEntity> Properties { get; set; }
    }
}