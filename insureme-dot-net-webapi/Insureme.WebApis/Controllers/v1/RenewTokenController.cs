using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Insureme.Core.v1.Helpers;
using Insureme.Core.v1.Objects;
using Insureme.Core.v1.Objects.Exceptions;
using Insureme.Core.v1.Objects.Responses;
using Insureme.Core.v1.Objects.Responses.Common;
using Insureme.DataAccess.Interfaces;
using Insureme.WebApis.Filters.Authorization;
using Insureme.WebApis.Properties;

namespace Insureme.WebApis.Controllers.v1
{
    [RoutePrefix("api/v1/renew-token")]
    [KeyAuthorization]
    public class RenewTokenController : ApiController
    {
        private readonly IDataService dataService;

        public RenewTokenController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            var email = default(string);

            try
            {
                var token = string.Empty;

                if (Request.Headers.Contains(Resources.TokenHeaderKey1))
                {
                    token = Request.Headers.GetValues(Resources.TokenHeaderKey1).FirstOrDefault();
                }
                else if (Request.Headers.Contains(Resources.TokenHeaderKey2))
                {
                    token = Request.Headers.GetValues(Resources.TokenHeaderKey2).FirstOrDefault();
                }

                if (string.IsNullOrEmpty(token))
                {
                    throw new GeneralException(10101, "Token is missing!");
                }

                var tokenString = token.Decrypt();
                var tokenStringParts = tokenString.Split(new char[] {'|'});

                if (tokenStringParts.Length == 9)
                {
                    var sysKey = tokenStringParts[0];
                    email = tokenStringParts[3].Trim().ToLower();
                    
                    if (!sysKey.Equals(WebSystem.BackEndKey, StringComparison.CurrentCultureIgnoreCase))
                    {
                        throw new GeneralException(10102, "Token is invalid! Token belongs to another system.");
                    }
                }
            }
            catch (GeneralException exception)
            {
                return new HttpActionResult<GenericResponse<string>>(
                      HttpStatusCode.Unauthorized,
                      new GenericResponse<string>
                      {
                          Error = new Error
                          {
                              ResponseCode = HttpStatusCode.Unauthorized,
                              Message = exception.Message
                          }
                      }
                );
            }
            catch (Exception)
            {
                return new HttpActionResult<GenericResponse<string>>(
                    HttpStatusCode.Unauthorized,
                    new GenericResponse<string>
                    {
                        Error = new Error
                        {
                            Code = 10105,
                            ResponseCode = HttpStatusCode.Unauthorized,
                            Message = "Token is invalid!"
                        }
                    }
                );
            }

            try
            {
                var newToken = dataService.GetToken(email);

                return new HttpActionResult<GenericResponse<string>>(
                    HttpStatusCode.OK,
                    new GenericResponse<string>
                    {
                        Result = newToken.Token
                    }
                );
            }
            catch (GeneralException exception)
            {
                return new HttpActionResult<GenericResponse<string>>(
                    HttpStatusCode.Unauthorized,
                    new GenericResponse<string>
                    {
                        Error = new Error
                        {
                            Code = exception.Code,
                            ResponseCode = HttpStatusCode.Unauthorized,
                            Message = exception.Message
                        }
                    }
                );
            }
            catch (Exception exception)
            {
                return new HttpActionResult<GenericResponse<string>>(
                    HttpStatusCode.InternalServerError,
                    new GenericResponse<string>
                    {
                        Error = new Error
                        {
                            Code = 10199,
                            ResponseCode = HttpStatusCode.InternalServerError,
                            Message = exception.Message
                        }
                    }
                );
            }
        }
    }
}