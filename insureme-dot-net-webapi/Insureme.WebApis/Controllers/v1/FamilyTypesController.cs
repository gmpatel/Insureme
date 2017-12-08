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
using Insureme.WebApis.Filters.Logging;

namespace Insureme.WebApis.Controllers.v1
{
    [RoutePrefix("api/v1/family-types")]
    [KeyAuthorization]
    public class FamilyTypesController : ApiController
    {
        private readonly IDataService dataService;

        public FamilyTypesController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var familyTypes = this.dataService.GetFamiltTypes();

                return new HttpActionResult<GenericResponse<IList<FamilyTypeEntity>>>(
                    HttpStatusCode.OK,
                    new GenericResponse<IList<FamilyTypeEntity>>
                    {
                        Result = !familyTypes.Any() 
                            ? null 
                            : familyTypes,

                        Error = familyTypes.Any() 
                            ? null 
                            : new Error
                                {
                                    Code = 10301,
                                    ResponseCode = HttpStatusCode.OK,
                                    Message = string.Format("There are no family types found!")
                                }
                    }
                );
            }
            catch (Exception exception)
            {
                return new HttpActionResult<GenericResponse<IList<FamilyTypeEntity>>>(
                    HttpStatusCode.InternalServerError,
                    new GenericResponse<IList<FamilyTypeEntity>>
                    {
                        Error = new Error
                        {
                            Code = 10399,
                            ResponseCode = HttpStatusCode.InternalServerError,
                            Message = exception.Message
                        }
                    }
                );
            }
        }
    }
}