using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Insureme.Core.v1.Entities;
using Insureme.Core.v1.Helpers;
using Insureme.Core.v1.Objects.Exceptions;
using Insureme.Core.v1.Objects.Responses.Common;
using Insureme.DataAccess.Interfaces;
using Insureme.WebApis.Filters.Authorization;

namespace Insureme.WebApis.Controllers.v1
{
    [RoutePrefix("api/v1/products")]
    [KeyAuthorization]
    public class ProductsController : ApiController
    {
        private readonly IDataService dataService;

        public ProductsController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        [Route("price-description-suggestions")]
        [HttpGet]
        public IHttpActionResult GetPriceDescriptions()
        {
            try
            {
                var p = this.dataService.GetPriceDescriptionSuggestions(null);

                return new HttpActionResult<GenericResponse<IList<string>>>(
                    HttpStatusCode.OK,
                    new GenericResponse<IList<string>>
                    {
                        Result = p
                    }
                );
            }
            catch (GeneralException exception)
            {
                return new HttpActionResult<GenericResponse<IList<string>>>(
                    HttpStatusCode.OK,
                    new GenericResponse<IList<string>>
                    {
                        Error = new Error
                        {
                            ResponseCode = HttpStatusCode.OK,
                            Code = exception.Code,
                            Message = exception.AllMessages()
                        }
                    }
                );
            }
            catch (Exception exception)
            {
                return new HttpActionResult<GenericResponse<IList<string>>>(
                    HttpStatusCode.InternalServerError,
                    new GenericResponse<IList<string>>
                    {
                        Error = new Error
                        {
                            ResponseCode = HttpStatusCode.InternalServerError,
                            Message = exception.AllMessages()
                        }
                    }
                );
            }
        }

        [Route("shipping-description-suggestions")]
        [HttpGet]
        public IHttpActionResult GetShippingDescriptions()
        {
            try
            {
                var p = this.dataService.GetShippingDescriptionSuggestions(null);

                return new HttpActionResult<GenericResponse<IList<string>>>(
                    HttpStatusCode.OK,
                    new GenericResponse<IList<string>>
                    {
                        Result = p
                    }
                );
            }
            catch (GeneralException exception)
            {
                return new HttpActionResult<GenericResponse<IList<string>>>(
                    HttpStatusCode.OK,
                    new GenericResponse<IList<string>>
                    {
                        Error = new Error
                        {
                            ResponseCode = HttpStatusCode.OK,
                            Code = exception.Code,
                            Message = exception.AllMessages()
                        }
                    }
                );
            }
            catch (Exception exception)
            {
                return new HttpActionResult<GenericResponse<IList<string>>>(
                    HttpStatusCode.InternalServerError,
                    new GenericResponse<IList<string>>
                    {
                        Error = new Error
                        {
                            ResponseCode = HttpStatusCode.InternalServerError,
                            Message = exception.AllMessages()
                        }
                    }
                );
            }
        }

        [TokenAuthorization("Admin, Client")]
        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateOrUpdateProduct(ProductEntity product)
        {
            try
            {
                var email = Request.GetEmail();
                var p = this.dataService.CreateOrUpdateProduct(email, product);
                
                return new HttpActionResult<GenericResponse<ProductEntity>>(
                    HttpStatusCode.OK,
                    new GenericResponse<ProductEntity>
                    {
                        Result = p
                    }
                );
            }
            catch (GeneralException exception)
            {
                return new HttpActionResult<GenericResponse<ProductEntity>>(
                    HttpStatusCode.OK,
                    new GenericResponse<ProductEntity>
                    {
                        Error = new Error
                        {
                            ResponseCode = HttpStatusCode.OK,
                            Code = exception.Code,
                            Message = exception.AllMessages()
                        }
                    }
                );
            }
            catch (Exception exception)
            {
                return new HttpActionResult<GenericResponse<ProductEntity>>(
                    HttpStatusCode.InternalServerError,
                    new GenericResponse<ProductEntity>
                    {
                        Error = new Error
                        {
                            ResponseCode = HttpStatusCode.InternalServerError,
                            Message = exception.AllMessages()
                        }
                    }
                );
            }
        }

        [TokenAuthorization("Admin, Client")]
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteProduct(long id)
        {
            try
            {
                var email = Request.GetEmail();
                var p = this.dataService.DeleteProduct(email, id, null);

                return new HttpActionResult<GenericResponse<bool>>(
                    HttpStatusCode.OK,
                    new GenericResponse<bool>
                    {
                        Result = p.Id.Equals(id)
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
                            Message = exception.AllMessages()
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
                            Message = exception.AllMessages()
                        }
                    }
                );
            }
        }
    }
}