using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Insureme.Core.v1.Objects.Requests.Dipn
{
    public class LinkRequest
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("connectionCode")]
        public string ConnectionCode { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }
    }
}