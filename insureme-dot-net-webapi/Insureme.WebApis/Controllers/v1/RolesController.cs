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
    [RoutePrefix("api/v1/roles")]
    [KeyAuthorization]
    public class RolesController : ApiController
    {
        private readonly IDataService dataService;

        public RolesController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult All()
        {
            try
            {
                var roles = this.dataService.GetRoles();

                return new HttpActionResult<GenericResponse<IList<RoleEntity>>>(
                        HttpStatusCode.OK,
                        new GenericResponse<IList<RoleEntity>>
                        {
                            Result = !roles.Any() 
                                ? null 
                                : roles,

                            Error = roles.Any() 
                                ? null 
                                : new Error
                                    {
                                        Code = 10501,
                                        ResponseCode = HttpStatusCode.NoContent,
                                        Message = string.Format("There are no roles found!"),
                                    }
                        }
                    );
            }
            catch (Exception exception)
            {
                return new HttpActionResult<GenericResponse<IList<RoleEntity>>>(
                    HttpStatusCode.InternalServerError,
                    new GenericResponse<IList<RoleEntity>>
                    {
                        Error = new Error
                        {
                            Code = 10599,
                            ResponseCode = HttpStatusCode.InternalServerError,
                            Message = exception.Message
                        }
                    }
                );
            }
        }
    }
}