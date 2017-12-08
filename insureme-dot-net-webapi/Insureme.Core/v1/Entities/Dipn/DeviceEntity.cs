using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insureme.Core.v1.Entities.Dipn
{
    public class DeviceEntity
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "registrationToken")]
        public string RegistrationToken { get; set; }
        
        [NotMapped]
        [JsonProperty(PropertyName = "type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string TypeString => Type.Name;

        [JsonProperty(PropertyName = "typeId", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long TypeId { get; set; }

        [JsonProperty(PropertyName = "enabled", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Enabled { get; set; }

        [JsonIgnore]
        public DateTime DateTimeCreated { get; set; }

        [JsonIgnore]
        public DateTime? DateTimeLastModified { get; set; }

        [JsonIgnore]
        public virtual DeviceTypeEntity Type { get; set; }

        [JsonIgnore]
        public virtual ICollection<AppEntity> Apps { get; set; }
    }
}