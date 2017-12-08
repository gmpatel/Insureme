using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Insureme.Core.v1.Entities;
using Insureme.Core.v1.Helpers;
using Insureme.Core.v1.Objects;
using Insureme.Core.v1.Objects.Responses;
using Insureme.Core.v1.Objects.Responses.Common;
using Insureme.DataAccess.Interfaces;
using Insureme.WebApis.Filters.Authorization;

namespace Insureme.WebApis.Controllers.v1
{
    [RoutePrefix("api/v1/states")]
    [KeyAuthorization]
    public class StatesController : ApiController
    {
        private readonly IDataService dataService;

        public StatesController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult All()
        {
            try
            {
                var states = this.dataService.GetStates();

                return new HttpActionResult<GenericResponse<IList<StateEntity>>>(
                    HttpStatusCode.OK,
                    new GenericResponse<IList<StateEntity>>
                    {
                        Result = !states.Any() 
                            ? null : 
                            states,

                        Error = states.Any() 
                            ? null 
                            : new Error
                                {
                                    Code = 10201,
                                    ResponseCode = HttpStatusCode.NoContent,
                                    Message = string.Format("There are no states found!")
                                }
                    }
                );
            }
            catch (Exception exception)
            {
                return new HttpActionResult<GenericResponse<StatesResponse>>(
                    HttpStatusCode.InternalServerError,
                    new GenericResponse<StatesResponse>
                    {
                        Error = new Error
                        {
                            Code = 10299,
                            ResponseCode = HttpStatusCode.InternalServerError,
                            Message = exception.Message
                        }
                    }
                );
            }
        }
    }
}