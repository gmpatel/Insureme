using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Insureme.Core.v1.Entities
{
    public class ListItemEntity
    {
        [JsonIgnore]
        public long Id { get; set; }

        [JsonProperty(Order = 1, PropertyName = "id")]
        public long ItemId { get; set; }

        [JsonProperty(Order = 2, PropertyName = "name")]
        public string Name { get; set; }

        [JsonIgnore]
        public long ListId { get; set; }

        [JsonIgnore]
        public bool Enabled { get; set; }

        [JsonIgnore]
        public DateTime DateTimeCreated { get; set; }

        [JsonIgnore]
        public DateTime? DateTimeLastModified { get; set; }

        [JsonIgnore]
        public virtual ListEntity List { get; set; }
    }
}