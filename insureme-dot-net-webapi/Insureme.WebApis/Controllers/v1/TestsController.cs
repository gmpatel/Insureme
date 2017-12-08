using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Insureme.Core.v1.Objects.Responses.Common;
using System.Web;

namespace Insureme.WebApis.Controllers.v1
{
    [RoutePrefix("api/v1/tests")]
    public class TestsController : ApiController
    {
        [Route("dynamic")]
        [HttpGet]
        public IHttpActionResult Dynamic()
        {
            return new HttpActionResult<GenericResponse<dynamic>>(
                HttpStatusCode.OK,
                new GenericResponse<dynamic>
                {
                    Result = DateTime.Now
                }
            );
        }

        [Route("ip")]
        [HttpGet]
        public IHttpActionResult Ip()
        {
            string clientAddress = HttpContext.Current.Request.UserHostAddress;

            return new HttpActionResult<GenericResponse<string>>(
                HttpStatusCode.OK,
                new GenericResponse<string>
                {
                    Result = clientAddress
                }
            );
        }   
    }

    public static class HttpRequestMessageExtensions
    {
        
    }
}