using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insureme.Core.v1.Entities.Dipn
{
    public class IPEntity
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string IPAddress { get; set; }

        [JsonProperty(PropertyName = "appId")]
        public Guid AppId { get; set; }

        [NotMapped]
        [JsonProperty(PropertyName = "app")]
        public string AppString => App.Name;

        [JsonIgnore]
        public DateTime DateTimeCreated { get; set; }

        [JsonIgnore]
        public DateTime? DateTimeLastModified { get; set; }

        [JsonIgnore]
        public virtual AppEntity App { get; set; }
    }
}