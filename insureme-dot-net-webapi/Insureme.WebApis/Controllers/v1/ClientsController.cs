using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Insureme.Core.v1.Entities;
using Insureme.Core.v1.Helpers;
using Insureme.Core.v1.Objects.Exceptions;
using Insureme.Core.v1.Objects.Responses;
using Insureme.Core.v1.Objects.Responses.Common;
using Insureme.DataAccess.Interfaces;
using Insureme.WebApis.Filters.Authorization;

namespace Insureme.WebApis.Controllers.v1
{
    [RoutePrefix("api/v1/clients")]
    [KeyAuthorization]
    public class ClientsController : ApiController
    {
        private readonly IDataService dataService;

        public ClientsController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        [TokenAuthorization("Admin, Client")]
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetClients()
        {
            try
            {
                var email = Request.GetEmail();
                var clients = this.dataService.GetClients(email);
                var root = Request.RequestUri.GetLeftPart(UriPartial.Authority);

                foreach (var client in clients)
                {
                    client.LogoUrlString = string.IsNullOrEmpty(client.LogoUrl)
                        ? string.Format(@"{0}/assets/img/logos/{1}.png", root, client.Code.ToLower())
                        : client.LogoUrl;
                }

                return new HttpActionResult<GenericResponse<IList<ClientEntity>>>(
                    HttpStatusCode.OK,
                    new GenericResponse<IList<ClientEntity>>
                    {
                        Result = clients
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

        [TokenAuthorization("Admin, Client")]
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetClient(long id)
        {
            try
            {
                var email = Request.GetEmail();
                var client = this.dataService.GetClient(email, id);
                var root = Request.RequestUri.GetLeftPart(UriPartial.Authority);

                client.LogoUrlString = string.IsNullOrEmpty(client.LogoUrl)
                    ? string.Format(@"{0}/assets/img/logos/{1}.png", root, client.Code.ToLower())
                    : client.LogoUrl;

                return new HttpActionResult<GenericResponse<ClientEntity>>(
                    HttpStatusCode.OK,
                    new GenericResponse<ClientEntity>
                    {
                        Result = client
                    }
                );
            }
            catch (GeneralException exception)
            {
                return new HttpActionResult<GenericResponse<ClientEntity>>(
                    HttpStatusCode.OK,
                    new GenericResponse<ClientEntity>
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
                return new HttpActionResult<GenericResponse<ClientEntity>>(
                   HttpStatusCode.InternalServerError,
                   new GenericResponse<ClientEntity>
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

        [TokenAuthorization("Admin, Client")]
        [Route("{id}/product-types")]
        [HttpGet]
        public IHttpActionResult GetClientProductTypes(long id)
        {
            try
            {
                var email = Request.GetEmail();
                var productTypes = this.dataService.GetProductTypes(email, id);

                return new HttpActionResult<GenericResponse<IList<ProductTypeEntity>>>(
                    HttpStatusCode.OK,
                    new GenericResponse<IList<ProductTypeEntity>>
                    {
                        Result = productTypes
                    }
                );
            }
            catch (GeneralException exception)
            {
                return new HttpActionResult<GenericResponse<IList<ProductTypeEntity>>>(
                    HttpStatusCode.OK,
                    new GenericResponse<IList<ProductTypeEntity>>
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
                return new HttpActionResult<GenericResponse<IList<ProductTypeEntity>>>(
                   HttpStatusCode.InternalServerError,
                   new GenericResponse<IList<ProductTypeEntity>>
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

        [TokenAuthorization("Admin, Client")]
        [Route("{id}/product-types/{id2}")]
        [HttpGet]
        public IHttpActionResult GetClientProductType(long id, long id2)
        {
            try
            {
                var email = Request.GetEmail();
                var productType = this.dataService.GetProductType(email, id, id2);

                return new HttpActionResult<GenericResponse<ProductTypeEntity>>(
                    HttpStatusCode.OK,
                    new GenericResponse<ProductTypeEntity>
                    {
                        Result = productType
                    }
                );
            }
            catch (GeneralException exception)
            {
                return new HttpActionResult<GenericResponse<ProductTypeEntity>>(
                     HttpStatusCode.OK,
                     new GenericResponse<ProductTypeEntity>
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
                return new HttpActionResult<GenericResponse<ProductTypeEntity>>(
                     HttpStatusCode.InternalServerError,
                     new GenericResponse<ProductTypeEntity>
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

        [TokenAuthorization("Admin, Client")]
        [Route("{id}/product-types/{id2}/data-sheet")]
        [HttpGet]
        public IHttpActionResult GetClientProductTypeWithDataSheet(long id, long id2)
        {
            try
            {
                var email = Request.GetEmail();
                var productType = this.dataService.GetProductType(email, id, id2);

                productType.DataSheet = DataSheetEntity.Load(productType);

                return new HttpActionResult<GenericResponse<ProductTypeEntity>>(
                    HttpStatusCode.OK,
                    new GenericResponse<ProductTypeEntity>
                    {
                        Result = productType
                    }
                );
            }
            catch (GeneralException exception)
            {
                return new HttpActionResult<GenericResponse<ProductTypeEntity>>(
                     HttpStatusCode.OK,
                     new GenericResponse<ProductTypeEntity>
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
                return new HttpActionResult<GenericResponse<ProductTypeEntity>>(
                     HttpStatusCode.InternalServerError,
                     new GenericResponse<ProductTypeEntity>
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

        [TokenAuthorization("Admin, Client")]
        [Route("{id}/products")]
        [HttpGet]
        public IHttpActionResult GetClientProducts(long id)
        {
            try
            {
                var email = Request.GetEmail();
                var root = Request.RequestUri.GetLeftPart(UriPartial.Authority);

                var products = this.dataService.GetProducts(email, id);

                foreach (var product in products)
                {
                    product.PriceDescriptionSuggestions = this.dataService.GetPriceDescriptionSuggestions(null);
                    product.ShippingDescriptionSuggestions = this.dataService.GetShippingDescriptionSuggestions(null);
                    product.Client.LogoUrlString = string.IsNullOrEmpty(product.Client.LogoUrl)
                        ? string.Format(@"{0}/assets/img/logos/{1}.png", root, product.Client.Code.ToLower())
                        : product.Client.LogoUrl;
                }

                return new HttpActionResult<GenericResponse<IList<ProductEntity>>>(
                    HttpStatusCode.OK,
                    new GenericResponse<IList<ProductEntity>>
                    {
                        Result = products
                    }
                );
            }
            catch (GeneralException exception)
            {
                return new HttpActionResult<GenericResponse<IList<ProductEntity>>>(
                    HttpStatusCode.OK,
                    new GenericResponse<IList<ProductEntity>>
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
                return new HttpActionResult<GenericResponse<IList<ProductEntity>>>(
                   HttpStatusCode.InternalServerError,
                   new GenericResponse<IList<ProductEntity>>
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

        [TokenAuthorization("Admin, Client")]
        [Route("{id}/products/{id2}")]
        [HttpGet]
        public IHttpActionResult GetClientProduct(long id, long id2)
        {
            try
            {
                var email = Request.GetEmail();
                var root = Request.RequestUri.GetLeftPart(UriPartial.Authority);

                var product = this.dataService.GetProduct(email, id, id2);
                product.PriceDescriptionSuggestions = this.dataService.GetPriceDescriptionSuggestions(null);
                product.ShippingDescriptionSuggestions = this.dataService.GetShippingDescriptionSuggestions(null);
                product.Client.LogoUrlString = string.IsNullOrEmpty(product.Client.LogoUrl)
                    ? string.Format(@"{0}/assets/img/logos/{1}.png", root, product.Client.Code.ToLower())
                    : product.Client.LogoUrl;

                return new HttpActionResult<GenericResponse<ProductEntity>>(
                    HttpStatusCode.OK,
                    new GenericResponse<ProductEntity>
                    {
                        Result = product
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
                             Message = exception.Message
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
                             Message = exception.Message
                         }
                     }
                );
            }
        }

        [TokenAuthorization("Admin, Client")]
        [Route("{id}/products/{id2}/data-sheet")]
        [HttpGet]
        public IHttpActionResult GetClientProductWithDataSheet(long id, long id2)
        {
            try
            {
                var email = Request.GetEmail();
                var root = Request.RequestUri.GetLeftPart(UriPartial.Authority);

                var product = this.dataService.GetProduct(email, id, id2);
                product.PriceDescriptionSuggestions = this.dataService.GetPriceDescriptionSuggestions(null);
                product.ShippingDescriptionSuggestions = this.dataService.GetShippingDescriptionSuggestions(null);
                product.Client.LogoUrlString = string.IsNullOrEmpty(product.Client.LogoUrl)
                    ? string.Format(@"{0}/assets/img/logos/{1}.png", root, product.Client.Code.ToLower())
                    : product.Client.LogoUrl;
                product.DataSheet = DataSheetEntity.Load(product);
                
                return new HttpActionResult<GenericResponse<ProductEntity>>(
                    HttpStatusCode.OK,
                    new GenericResponse<ProductEntity>
                    {
                        Result = product
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
                             Message = exception.Message
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
                             Message = exception.Message
                         }
                     }
                );
            }
        }

        [TokenAuthorization("Admin, Client")]
        [Route("{id}/products/{id2}")]
        [HttpDelete]
        public IHttpActionResult DeleteClientProduct(long id, long id2)
        {
            try
            {
                var email = Request.GetEmail();
                var product = this.dataService.DeleteProduct(email, id2, id);
                
                return new HttpActionResult<GenericResponse<bool>>(
                    HttpStatusCode.OK,
                    new GenericResponse<bool>
                    {
                        Result = product.Id.Equals(id2)
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