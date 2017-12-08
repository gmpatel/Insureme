using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;                  
using Insureme.WebApis.Filters.Authorization;

namespace Insureme.WebApis.Controllers.v1
{
    [RoutePrefix("api/v1/values")]
    [KeyAuthorization]
    public class ValuesController : ApiController
    {
        [Route("")]
        [HttpGet]
        [TokenAuthorization("Client, Admin")]
        public IHttpActionResult Get()
        {
            return new HttpActionResult<string[]>(HttpStatusCode.OK, new [] { "value 1", "value 2", "value 3", "value 4", "value 5" });
        }
                                
        [Route("id/{id}")]
        [HttpGet]
        [TokenAuthorization("*")]
        public IHttpActionResult Get(int id)
        {
            return new HttpActionResult<string>(HttpStatusCode.OK, string.Format("value {0}", id));
        }

        [Route("test")]
        [HttpGet]
        public HttpResponseMessage Test()
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent("<html><body>Hello World</body></html>")
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

            return response;
        }
    }
}