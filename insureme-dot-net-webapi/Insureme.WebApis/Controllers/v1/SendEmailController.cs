using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.WebPages;
using Insureme.Core.v1.Helpers;
using Insureme.Core.v1.Objects;
using Insureme.Core.v1.Objects.Requests;
using Insureme.Core.v1.Objects.Responses;
using Insureme.Core.v1.Objects.Responses.Common;
using Insureme.WebApis.Filters.Authorization;

namespace Insureme.WebApis.Controllers.v1
{
    [RoutePrefix("api/v1/send-email")]
    [KeyAuthorization]
    public class SendEmailController : ApiController
    {
        internal static class Constants
        {
            public const string MutexStringDummyEmail = "82e22326-6c5d-4582-92cd-178104648110 insureme.com.au email-controller one-at-a-time dummy-email";
        }
                            
        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(EmailRequest request)
        {
            try
            {
                var response = request.SendEmail();

                return new HttpActionResult<GenericResponse<bool>>(
                    HttpStatusCode.OK,
                    new GenericResponse<bool>
                    {
                        Result = response
                    }
                );
            }
            catch (Exception exception)
            {
                return new HttpActionResult<GenericResponse<bool>>(
                    HttpStatusCode.InternalServerError,
                    new GenericResponse<bool>
                    {
                        Error = new Error
                        {
                            Code = 99999,
                            ResponseCode = HttpStatusCode.BadRequest,
                            Message = exception.Message
                        }
                    }
                );
            }
        }
                           
        [Route("dummy")]
        [HttpPost]
        public IHttpActionResult Dummy(EmailRequest request)
        {
            using (var mutex = new Mutex(false, string.Format("{0}", Constants.MutexStringDummyEmail)))
            {
                try
                {
                    if (!mutex.WaitOne(new TimeSpan(1, 0, 0, 0), false))
                    {
                        throw new Exception(string.Format("One of the previous threads executing this function 'DummyEmail()' did not release the mutex '{0}'!", string.Format("{0}", Constants.MutexStringDummyEmail)));
                    }

                    var email = request?.Body.Replace("Email=", "").Replace("email=", "").Replace("EMAIL=", "").Replace("%40", "@");

                    if (!string.IsNullOrEmpty(email))
                    {
                        if (email.IsValidEmail())
                        {
                            File.AppendAllText(string.Format("{0}{1}", HttpRuntime.AppDomainAppPath, "Contacts.txt"), string.Format("{0},{1}{2}{3}",DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff tt"),"\t",email,Environment.NewLine));

                            return new HttpActionResult<GenericResponse<bool>>(
                                HttpStatusCode.OK, 
                                new GenericResponse<bool>
                                {
                                    Result = true
                                }
                            );
                        }
                        else
                        {
                            return new HttpActionResult<GenericResponse<bool>>(
                                HttpStatusCode.OK,
                                new GenericResponse<bool>
                                {
                                    Result = false
                                }
                            );
                        }
                    }

                    return new HttpActionResult<GenericResponse<bool>>(
                        HttpStatusCode.BadRequest,
                        new GenericResponse<bool>
                        {
                            Error = new Error
                            {
                                Code = 99998,
                                ResponseCode = HttpStatusCode.BadRequest,
                                Message = string.Format("Invalid email address or email not provided")
                            }
                        }
                    );
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
        }
    }
}