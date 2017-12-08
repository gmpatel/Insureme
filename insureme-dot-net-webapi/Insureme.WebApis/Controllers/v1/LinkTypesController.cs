using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Insureme.Core.v1.Entities;
using Insureme.Core.v1.Objects.Responses.Common;
using Insureme.DataAccess.Interfaces;
using Insureme.WebApis.Filters.Authorization;

namespace Insureme.WebApis.Controllers.v1
{
    [RoutePrefix("api/v1/link-types")]
    [KeyAuthorization]
    public class LinkTypesController : ApiController
    {
        private readonly IDataService dataService;

        public LinkTypesController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var linkTypes = this.dataService.GetLinkTypes();

                return new HttpActionResult<GenericResponse<IList<LinkTypeEntity>>>(
                    HttpStatusCode.OK,
                    new GenericResponse<IList<LinkTypeEntity>>
                    {
                        Result = !linkTypes.Any()
                            ? null
                            : linkTypes,

                        Error = linkTypes.Any()
                            ? null
                            : new Error
                            {
                                Code = 10301,
                                ResponseCode = HttpStatusCode.OK,
                                Message = string.Format("There are no link types found!")
                            }
                    }
                );
            }
            catch (Exception exception)
            {
                return new HttpActionResult<GenericResponse<IList<LinkTypeEntity>>>(
                    HttpStatusCode.InternalServerError,
                    new GenericResponse<IList<LinkTypeEntity>>
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