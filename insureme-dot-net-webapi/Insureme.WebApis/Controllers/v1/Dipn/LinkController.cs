using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Insureme.Core.v1.Entities.Dipn;
using Insureme.Core.v1.Helpers;
using Insureme.Core.v1.Objects.Exceptions;
using Insureme.Core.v1.Objects.Requests.Dipn;
using Insureme.Core.v1.Objects.Responses.Common;
using Insureme.DataAccess.Interfaces;
using Insureme.WebApis.Filters.Authorization;

namespace Insureme.WebApis.Controllers.v1.Dipn
{
    [RoutePrefix("api/dipn/v1/link/device/app")]
    [KeyAuthorization]
    public class LinkController : ApiController
    {
        private readonly IDataService dataService;

        public LinkController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        [TokenAuthorization("*")]
        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(LinkRequest request)
        {
            try
            {
                var result = dataService.DipnLinkDeviceWithApp(request.Id, request.ConnectionCode, request.Key);

                return new HttpActionResult<GenericResponse<bool>>(
                    HttpStatusCode.OK,
                    new GenericResponse<bool>
                    {
                        Result = result
                    }
                );
            }
            catch (GeneralException exception)
            {
                return new HttpActionResult<GenericResponse<bool>>(
                    HttpStatusCode.OK,
                    new GenericResponse<bool>
                    {
                        Error = new Error
                        {
                            ResponseCode = HttpStatusCode.OK,
                            Code = exception.Code,
                            Message = exception.Message
                        }
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
                            ResponseCode = HttpStatusCode.InternalServerError,
                            Message = exception.Message
                        }
                    }
                );
            }
        }
    }
}