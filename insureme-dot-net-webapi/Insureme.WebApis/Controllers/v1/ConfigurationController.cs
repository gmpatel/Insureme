using Insureme.Core.v1.Entities;
using Insureme.Core.v1.Objects.Exceptions;
using Insureme.Core.v1.Objects.Responses.Common;
using Insureme.DataAccess.Interfaces;
using Insureme.WebApis.Filters.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Insureme.WebApis.Controllers.v1
{
    [RoutePrefix("api/v1/configurations")]
    [KeyAuthorization]
    public class ConfigurationController : ApiController
    {
        private readonly IDataService dataService;

        public ConfigurationController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        //[TokenAuthorization("*")]
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetConfigurations()
        {
            try
            {
                var configurations = dataService.GetConfigurations();

                return new HttpActionResult<GenericResponse<ConfigurationEntity>>(
                    HttpStatusCode.OK,
                    new GenericResponse<ConfigurationEntity>
                    {
                        Result = configurations
                    }
                );
            }
            catch (GeneralException exception)
            {
                return new HttpActionResult<GenericResponse<IList<ClientEntity>>>(
                    HttpStatusCode.OK,
                    new GenericResponse<IList<ClientEntity>>
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
                return new HttpActionResult<GenericResponse<IList<ClientEntity>>>(
                    HttpStatusCode.InternalServerError,
                    new GenericResponse<IList<ClientEntity>>
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