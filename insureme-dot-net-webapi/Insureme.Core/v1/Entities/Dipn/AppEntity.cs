using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insureme.Core.v1.Entities.Dipn
{
    public class AppEntity
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonIgnore]
        //[JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [NotMapped]
        [JsonProperty(PropertyName = "type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string TypeString => Type.Name;

        [JsonProperty(PropertyName = "typeId", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long TypeId { get; set; }

        [JsonProperty(PropertyName = "connectionCode", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ConnectionCode { get; set; }

        [JsonProperty(PropertyName = "key", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Key { get; set; }

        [NotMapped]
        [JsonProperty(PropertyName = "ip", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string IPString
        {
            get
            {
                var ipEntity = IPs.OrderByDescending(x => x.DateTimeCreated).FirstOrDefault();

                if (ipEntity != null)
                {
                    return ipEntity.IPAddress;
                }

                return null;
            }
        }

        [NotMapped]
        [JsonProperty(PropertyName = "updateTime", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? UpdateTime
        {
            get
            {
                var ipEntity = IPs.OrderByDescending(x => x.DateTimeCreated).FirstOrDefault();

                if (ipEntity != null)
                {
                    return ipEntity.DateTimeLastModified.Value;
                }

                return null;
            }
        }
        
        [JsonIgnore]
        //[JsonProperty(PropertyName = "previousKey", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string PreviousKey { get; set; }

        [JsonIgnore]
        public DateTime DateTimeCreated { get; set; }

        [JsonIgnore]
        public DateTime? DateTimeLastModified { get; set; }

        [JsonIgnore]
        public virtual AppTypeEntity Type { get; set; }

        [JsonIgnore]
        public virtual ICollection<DeviceEntity> Devices { get; set; }

        [JsonIgnore]
        public virtual ICollection<IPEntity> IPs { get; set; }
    }
}