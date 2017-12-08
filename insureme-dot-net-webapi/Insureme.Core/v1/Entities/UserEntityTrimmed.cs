using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Insureme.Core.v1.Entities
{
    public class UserEntityTrimmed
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "role", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Role { get; set; }

        [JsonProperty(PropertyName = "enabled", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? Enabled { get; set; }

        [JsonProperty(PropertyName = "verified", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? Verified { get; set; }
    }
}