using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insureme.Core.v1.Objects.Responses;
using Newtonsoft.Json;

namespace Insureme.Core.v1.Entities
{
    public class ProductTypeEntity
    {
        [JsonProperty(PropertyName = "id", Order = 1)]
        public long Id { get; set; }

        [JsonIgnore]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "title", Order = 2)]
        public string Title { get; set; }

        [NotMapped]
        [JsonProperty(PropertyName = "dataSheet", Order = 3, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual DataSheetEntity DataSheet { get; set; }

        [JsonIgnore]
        public bool Enabled { get; set; }

        [JsonIgnore]
        public DateTime DateTimeCreated { get; set; }

        [JsonIgnore]
        public DateTime? DateTimeLastModified { get; set; }

        [JsonIgnore]
        public virtual ICollection<ClientEntity> Clients { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<ProductEntity> Products { get; set; }

        [JsonIgnore]
        public virtual ICollection<ZoneEntity> Zones { get; set; }
    }
}