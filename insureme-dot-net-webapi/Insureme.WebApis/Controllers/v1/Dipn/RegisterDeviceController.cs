using Insureme.Core.v1.Entities.Dipn;
using Insureme.Core.v1.Helpers;
using Insureme.Core.v1.Objects.Exceptions;
using Insureme.Core.v1.Objects.Requests.Dipn;
using Insureme.Core.v1.Objects.Responses.Common;
using Insureme.DataAccess.Interfaces;
using Insureme.WebApis.Filters.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Insureme.WebApis.Controllers.v1.Dipn
{
    [RoutePrefix("api/dipn/v1/register/device")]
    [KeyAuthorization]
    public class RegisterDeviceController : ApiController
    {
        private readonly IDataService dataService;

        public RegisterDeviceController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        [TokenAuthorization("*")]
        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(RegisterDeviceRequest request)
        {
            try
            {
                var result = dataService.DipnRegisterDevice(request.Id, request.RegistrationToken, request.Type);

                return new HttpActionResult<GenericResponse<DeviceEntity>>(
                    HttpStatusCode.OK,
                    new GenericResponse<DeviceEntity>
                    {
                        Result = result
                    }
                );
            }
            catch (GeneralException exception)
            {
                return new HttpActionResult<GenericResponse<DeviceEntity>>(
                    HttpStatusCode.OK,
                    new GenericResponse<DeviceEntity>
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
                return new HttpActionResult<GenericResponse<DeviceEntity>>(
                    HttpStatusCode.InternalServerError,
                    new GenericResponse<DeviceEntity>
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