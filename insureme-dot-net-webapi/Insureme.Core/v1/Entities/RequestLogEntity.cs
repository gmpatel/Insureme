using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Insureme.Core.v1.Entities
{
    public class RequestLogEntity
    {
        public long Id { get; set; }
        public string Method { get; set; }           // The request method (GET, POST, etc).
        public string Uri { get; set; }              // The request URI.
        public string IpAddress { get; set; }        // The IP address that made the request.
        public string Headers { get; set; }          // The request headers.
        public string Key { get; set; }             // The application that made the request.
        public string Token { get; set; }             // The application that made the request.
        public string ContentType { get; set; }      // The request content type.
        public string ContentBody { get; set; }      // The request content body.
        public DateTime? DateTimeRequested { get; set; }     // The request timestamp.
        public int? ResponseStatusCode { get; set; }        // The response status code.
        public string ResponseHeaders { get; set; }         // The response headers.
        public string ResponseContentType { get; set; }     // The response content type.
        public string ResponseContentBody { get; set; }     // The response content body.
        public DateTime? DateTimeResponded { get; set; }    // The response timestamp.
    }
}