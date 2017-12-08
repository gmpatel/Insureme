using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Insureme.Core.v1.Entities
{
    public class ClientEntity
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [NotMapped]
        [JsonProperty(PropertyName = "logoUrl", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string LogoUrlString { get; set; }

        [JsonIgnore]
        public string LogoUrl { get; set; }

        [JsonIgnore]
        public bool Enabled { get; set; }

        [JsonIgnore]
        public DateTime DateTimeCreated { get; set; }

        [JsonIgnore]
        public DateTime? DateTimeLastModified { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProductTypeEntity> ProductTypes { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProductEntity> Products { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserEntity> Users { get; set; }
    }
}
