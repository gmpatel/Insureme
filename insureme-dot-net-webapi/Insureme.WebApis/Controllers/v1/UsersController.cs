using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Web;
using System.Web.Http;
using Insureme.Core.v1.Entities;
using Insureme.Core.v1.Helpers;
using Insureme.Core.v1.Objects;
using Insureme.Core.v1.Objects.Requests;
using Insureme.Core.v1.Objects.Responses;
using Insureme.Core.v1.Objects.Responses.Common;
using Insureme.DataAccess.Interfaces;
using Insureme.WebApis.Filters.Authorization;
using Insureme.Core.v1.Objects.Exceptions;

namespace Insureme.WebApis.Controllers.v1
{
    [RoutePrefix("api/v1/users")]
    [KeyAuthorization]
    public class UsersController : ApiController
    {
        private readonly IDataService dataService;

        public UsersController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        [TokenAuthorization("Admin")]
        [Route("email")]
        [HttpGet]
        public IHttpActionResult Get(string address)
        {
            try
            {
                //var role = Request.GetRole() ?? string.Empty;

                //if (!role.Equals("Admin") || string.IsNullOrEmpty(address))
                //{
                //    address = Request.GetEmail();
                //}

                var user = this.dataService.GetUser(address);
                
                return new HttpActionResult<GenericResponse<UserEntity>>(
                    HttpStatusCode.OK,
                    new GenericResponse<UserEntity>
                    {
                        Result = user
                    }
                );
            }
            catch (GeneralException exception)
            {
                return new HttpActionResult<GenericResponse<UserEntity>>(
                    HttpStatusCode.OK,
                    new GenericResponse<UserEntity>
                    {
                        Error = new Error
                        {
                            Code = exception.Code,
                            ResponseCode = HttpStatusCode.OK,
                            Message = exception.Message
                        }
                    }
               );
            }
            catch (Exception exception)
            {
                return new HttpActionResult<GenericResponse<UserEntity>>(
                    HttpStatusCode.InternalServerError,
                    new GenericResponse<UserEntity>
                    {
                        Error = new Error
                        {
                            Code = 10699,
                            ResponseCode = HttpStatusCode.InternalServerError,
                            Message = exception.Message
                        }
                    }
               );
            }
        }

        [TokenAuthorization("Admin")]
        [Route("id/{id}")]
        [HttpGet]
        public IHttpActionResult Get(long id)
        {
            try
            {
                var user = this.dataService.GetUser(id);

                return new HttpActionResult<GenericResponse<UserEntity>>(
                    HttpStatusCode.OK,
                    new GenericResponse<UserEntity>
                    {
                        Result = user
                    }
                );
            }
            catch (GeneralException exception)
            {
                return new HttpActionResult<GenericResponse<UserEntity>>(
                    HttpStatusCode.OK,
                    new GenericResponse<UserEntity>
                    {
                        Error = new Error
                        {
                            Code = exception.Code,
                            ResponseCode = HttpStatusCode.OK,
                            Message = exception.Message
                        }
                    }
               );
            }
            catch (Exception exception)
            {
                return new HttpActionResult<GenericResponse<UserEntity>>(
                    HttpStatusCode.InternalServerError,
                    new GenericResponse<UserEntity>
                    {
                        Error = new Error
                        {
                            Code = 10699,
                            ResponseCode = HttpStatusCode.InternalServerError,
                            Message = exception.Message
                        }
                    }
               );
            }
        }
             
        [TokenAuthorization("Admin")]
        [Route("level/{level}")]
        [HttpGet]
        public IHttpActionResult Get(int level)
        {
            try
            {
                if (level == 3)
                {
                    var users = this.dataService.GetUsers();

                    return new HttpActionResult<GenericResponse<IList<UserEntity>>>(
                        HttpStatusCode.OK,
                        new GenericResponse<IList<UserEntity>>
                        {
                            Result = users
                        }
                    );
                }

                if (level == 1 || level == 2)
                {
                    var users = this.dataService.GetUsers(level);

                    return new HttpActionResult<GenericResponse<IList<UserEntityTrimmed>>>(
                        HttpStatusCode.OK,
                        new GenericResponse<IList<UserEntityTrimmed>>
                        {
                            Result = users
                        }
                    );
                }

                return new HttpActionResult<GenericResponse<IList<UserEntityTrimmed>>>(
                    HttpStatusCode.OK,
                    new GenericResponse<IList<UserEntityTrimmed>>
                    {
                        Error = new Error
                        {
                            Code = 10611,
                            ResponseCode = HttpStatusCode.OK,
                            Message = string.Format("Invalid level '{0}'. Please pass 1, 2 or 3.!", level)
            }
                    }
                );
            }
            catch (GeneralException exception)
            {
                return new HttpActionResult<GenericResponse<IList<UserEntityTrimmed>>>(
                    HttpStatusCode.OK,
                    new GenericResponse<IList<UserEntityTrimmed>>
                    {
                        Error = new Error
                        {
                            Code = exception.Code,
                            ResponseCode = HttpStatusCode.OK,
                            Message = exception.Message
                        }
                    }
                );
            }
            catch (Exception exception)
            {
                return new HttpActionResult<GenericResponse<IList<UserEntityTrimmed>>>(
                    HttpStatusCode.InternalServerError,
                    new GenericResponse<IList<UserEntityTrimmed>>
                    {
                        Error = new Error
                        {
                            Code = 10699,
                            ResponseCode = HttpStatusCode.InternalServerError,
                            Message = exception.Message
                        }
                    }
               );
            }
        }

        [TokenAuthorization]
        [Route("self")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var user = this.dataService.GetUser(Request.GetEmail());
                var token = this.dataService.GetToken(user);

                return new HttpActionResult<GenericResponse<LoginUserResponse>>(
                    HttpStatusCode.OK,
                    new GenericResponse<LoginUserResponse> 
                    {
                        Result = new LoginUserResponse
                        {
                            User = user,
                            Token = token.Token
                        }
                    }
                );
            }
            catch (GeneralException exception)
            {
                return new HttpActionResult<GenericResponse<LoginUserResponse>>(
                    HttpStatusCode.OK,
                    new GenericResponse<LoginUserResponse>
                    {
                        Error = new Error
                        {
                            Code = exception.Code,
                            ResponseCode = HttpStatusCode.OK,
                            Message = exception.Message
                        }
                    }
               );
            }
            catch (Exception exception)
            {
                return new HttpActionResult<GenericResponse<LoginUserResponse>>(
                    HttpStatusCode.InternalServerError,
                    new GenericResponse<LoginUserResponse>
                    {
                        Error = new Error
                        {
                            Code = 10699,
                            ResponseCode = HttpStatusCode.InternalServerError,
                            Message = exception.Message
                        }
                    }
               );
            }
        }
            
        [Route("verify")]
        [HttpGet]
        public HttpResponseMessage Verify(string key, string code)
        {
            try
            {
                var verified = default(bool);
                var user = this.dataService.VerifyUser(key, code, out verified);

                var response = new HttpResponseMessage
                {
                    Content = verified ? new StringContent(user.GetAccountVerifiedPage()) : new StringContent(user.GetAccountAlreadyVerifiedPage())
                };

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

                return response;
            }
            catch (Exception exception)
            {
                var response = new HttpResponseMessage
                {
                    Content = new StringContent(exception.GetAccountVerificationErrorPage())
                };

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

                return response;
            }
        }

        [Route("verify-json")]
        [HttpGet]
        public IHttpActionResult VerifyJson(string key, string code)
        {
            try
            {
                var verified = default(bool);
                var user = this.dataService.VerifyUser(key, code, out verified);

                return new HttpActionResult<GenericResponse<VerifyUserResponse>>(
                      HttpStatusCode.OK,
                      new GenericResponse<VerifyUserResponse>
                      {
                          Result = new VerifyUserResponse
                          {
                              AttemptToVerify = verified,
                              User = user
                          }
                      }
                 );
            }
            catch (GeneralException exception)
            {
                return new HttpActionResult<GenericResponse<VerifyUserResponse>>(
                      HttpStatusCode.OK,
                      new GenericResponse<VerifyUserResponse>
                      {
                          Error = new Error
                          {
                              Code = exception.Code,
                              ResponseCode = HttpStatusCode.OK,
                              Message = exception.Message
                          }
                      }
                 );
            }
            catch (Exception exception)
            {
                return new HttpActionResult<GenericResponse<VerifyUserResponse>>(
                    HttpStatusCode.InternalServerError,
                    new GenericResponse<VerifyUserResponse>
                    {
                        Error = new Error
                        {
                            Code = 10699,
                            ResponseCode = HttpStatusCode.InternalServerError,
                            Message = exception.Message
                        }
                    }
                );
            }
        }
                                               
        [TokenAuthorization("Admin")]
        [Route("send-verify-email")]
        [HttpGet]
        public IHttpActionResult SendVerifyEmail(string address)
        {
            try
            {
                //var role = Request.GetRole() ?? string.Empty;

                //if (!role.Equals("Admin"))
                //{
                //    email = Request.GetEmail();
                //}

                var root = Request.RequestUri.GetLeftPart(UriPartial.Authority);
                var user = this.dataService.GetUser(address);

                if (!user.Enabled)
                {
                    throw new GeneralException(10602, string.Format("User '{0}' has been marked as disabled!", user.Email));
                }

                if (user.Verified)
                {
                    throw new GeneralException(10604, string.Format("User '{0}' has been verified already!", user.Email));
                }

                var sent = user.SendVerificationEmail(root);

                return new HttpActionResult<GenericResponse<EmailResponse>>(
                      HttpStatusCode.OK,
                      new GenericResponse<EmailResponse>
                      {
                          Result = new EmailResponse
                          {
                              Sent = sent,
                              User = user
                          }
                      }
                 );
            }
            catch (GeneralException exception)
            {
                return new HttpActionResult<GenericResponse<EmailResponse>>(
                      HttpStatusCode.OK,
                      new GenericResponse<EmailResponse>
                      {
                          Error = new Error
                          {
                              Code = exception.Code,
                              ResponseCode = HttpStatusCode.OK,
                              Message = exception.Message
                          }
                      }
                 );
            }
            catch (Exception exception)
            {
                return new HttpActionResult<GenericResponse<EmailResponse>>(
                    HttpStatusCode.InternalServerError,
                    new GenericResponse<EmailResponse>
                    {
                        Error = new Error
                        {
                            Code = 10699,
                            ResponseCode = HttpStatusCode.InternalServerError,
                            Message = exception.Message
                        }
                    }
               );
            }
        }

        [TokenAuthorization("Admin")]
        [Route("send-verify-email/id/{id}")]
        [HttpGet]
        public IHttpActionResult SendVerifyEmail(long id)
        {
            try
            {
                //var role = Request.GetRole() ?? string.Empty;

                //if (!role.Equals("Admin"))
                //{
                //    email = Request.GetEmail();
                //}

                var root = Request.RequestUri.GetLeftPart(UriPartial.Authority);
                var user = this.dataService.GetUser(id);

                if (!user.Enabled)
                {
                    throw new GeneralException(10602, string.Format("User '{0}' has been marked as disabled!", user.Email));
                }

                if (user.Verified)
                {
                    throw new GeneralException(10604, string.Format("User '{0}' has been verified already!", user.Email));
                }

                var sent = user.SendVerificationEmail(root);

                return new HttpActionResult<GenericResponse<EmailResponse>>(
                      HttpStatusCode.OK,
                      new GenericResponse<EmailResponse>
                      {
                          Result = new EmailResponse
                          {
                              Sent = sent,
                              User = user
                          }
                      }
                 );
            }
            catch (GeneralException exception)
            {
                return new HttpActionResult<GenericResponse<EmailResponse>>(
                      HttpStatusCode.OK,
                      new GenericResponse<EmailResponse>
                      {
                          Error = new Error
                          {
                              Code = exception.Code,
                              ResponseCode = HttpStatusCode.OK,
                              Message = exception.Message
                          }
                      }
                 );
            }
            catch (Exception exception)
            {
                return new HttpActionResult<GenericResponse<EmailResponse>>(
                    HttpStatusCode.InternalServerError,
                    new GenericResponse<EmailResponse>
                    {
                        Error = new Error
                        {
                            Code = 10699,
                            ResponseCode = HttpStatusCode.InternalServerError,
                            Message = exception.Message
                        }
                    }
               );
            }
        }

        /*
        [Route("verify/get")]
        [HttpGet]
        public IHttpActionResult Verify(string key, string code)
        {
            try
            {
                var user = this.dataService.VerifyUser(key, code);

                return new HttpActionResult<GenericResponse<VerifyUserResponse>>(
                    HttpStatusCode.OK,
                    new GenericResponse<VerifyUserResponse>
                    {
                        Result = new VerifyUserResponse
                        {
                            User = user
                        }
                    }
                );
            }
            catch (GeneralException exception)
            {
                return new HttpActionResult<GenericResponse<VerifyUserResponse>>(
                    HttpStatusCode.BadRequest,
                    new GenericResponse<VerifyUserResponse>
                    {
                        Error = new Error
                        {
                            ResponseCode = HttpStatusCode.BadRequest,
                            Message = exception.Message
                        }
                    }
                );
            }
            catch (Exception exception)
            {
                return new HttpActionResult<GenericResponse<VerifyUserResponse>>(
                    HttpStatusCode.InternalServerError,
                    new GenericResponse<VerifyUserResponse>
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
        */

        [Route("sign-in")]
        [HttpPost]
        public IHttpActionResult SignIn(LoginUserRequest request)
        {
            try
            {
                var user = this.dataService.LoginUser(request);
                var token = this.dataService.GetToken(user);

                return new HttpActionResult<GenericResponse<LoginUserResponse>>(
                    HttpStatusCode.OK,
                    new GenericResponse<LoginUserResponse>
                    {
                        Result = new LoginUserResponse
                        {
                            User = user,
                            Token = token.Token
                        }
                    }
                );
            }
            catch (GeneralException exception)
            {
                return new HttpActionResult<GenericResponse<LoginUserResponse>>(
                    HttpStatusCode.OK,
                    new GenericResponse<LoginUserResponse>
                    {
                        Error = new Error
                        {
                            Code = exception.Code,
                            ResponseCode = HttpStatusCode.OK,
                            Message = exception.Message
                        }
                    }
                );
            }
            catch (Exception exception)
            {
                return new HttpActionResult<GenericResponse<LoginUserResponse>>(
                    HttpStatusCode.InternalServerError,
                    new GenericResponse<LoginUserResponse>
                    {
                        Error = new Error
                        {
                            Code = 10699,
                            ResponseCode = HttpStatusCode.InternalServerError,
                            Message = exception.Message
                        }
                    }
                );
            }
        }

        [Route("sign-up")]
        [HttpPost]
        public IHttpActionResult SignUp(RegisterUserRequest request)
        {
            try
            {
                var root = Request.RequestUri.GetLeftPart(UriPartial.Authority);
                var user = this.dataService.RegisterUser(request);

                var thread = new Thread(delegate() { user.SendVerificationEmail(root); });
                thread.Start();

                return new HttpActionResult<GenericResponse<UserEntity>>(
                    HttpStatusCode.OK,
                    new GenericResponse<UserEntity>
                    {
                        Result = user
                    }
                );
            }
            catch (GeneralException exception)
            {
                return new HttpActionResult<GenericResponse<UserEntity>>(
                    HttpStatusCode.OK,
                    new GenericResponse<UserEntity>
                    {
                        Error = new Error
                        {
                            Code = exception.Code,
                            ResponseCode = HttpStatusCode.OK,
                            Message = exception.Message
                        }
                    }
                );
            }
            catch (Exception exception)
            {
                return new HttpActionResult<GenericResponse<UserEntity>>(
                    HttpStatusCode.InternalServerError,
                    new GenericResponse<UserEntity>
                    {
                        Error = new Error
                        {
                            Code = 10699,
                            ResponseCode = HttpStatusCode.InternalServerError,
                            Message = exception.Message
                        }
                    }
                );
            }
        }
    }
}