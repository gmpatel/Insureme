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
    [RoutePrefix("api/v1/income-ranges")]
    [KeyAuthorization]
    public class IncomeRangesController : ApiController
    {
        private readonly IDataService dataService;

        public IncomeRangesController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult All()
        {
            try
            {
                var incomeRanges = this.dataService.GetIncomeRanges();

                return new HttpActionResult<GenericResponse<IList<IncomeRangeEntity>>>(
                    HttpStatusCode.OK,
                    new GenericResponse<IList<IncomeRangeEntity>>
                    {
                        Result = !incomeRanges.Any() 
                            ? null 
                            : incomeRanges,

                        Error = incomeRanges.Any() 
                            ? null 
                            : new Error
                                {
                                    Code = 10401,
                                    ResponseCode = HttpStatusCode.NoContent,
                                    Message = string.Format("There are no income ranges found!")
                                }
                    }
                );
            }
            catch (Exception exception)
            {
                return new HttpActionResult<GenericResponse<IList<IncomeRangeEntity>>>(
                    HttpStatusCode.InternalServerError,
                    new GenericResponse<IList<IncomeRangeEntity>>
                    {
                        Error = new Error
                        {
                            Code = 10499,
                            ResponseCode = HttpStatusCode.InternalServerError,
                            Message = exception.Message
                        }
                    }
                );
            }
        }
    }
}