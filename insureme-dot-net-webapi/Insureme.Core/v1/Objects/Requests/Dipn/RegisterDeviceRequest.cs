using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insureme.Core.v1.Objects.Requests.Dipn
{
    public class RegisterDeviceRequest
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("registrationToken")]
        public string RegistrationToken { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}