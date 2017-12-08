using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Routing;
using Autofac;
using Insureme.Core.v1.Entities;
using Insureme.Core.v1.Helpers;
using Insureme.DataAccess.Interfaces;
using Newtonsoft.Json;

namespace Insureme.WebApis.Filters.Logging
{
    public class ApiLogHandler : DelegatingHandler
    {
        private readonly IRepository<RequestLogEntity> requestLogsRepository;

        public ApiLogHandler()
        {
            this.requestLogsRepository = WebSystem.Container.Resolve<IRepository<RequestLogEntity>>();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestLogEntry = CreateApiLogEntryWithRequestData(request);

            if (request.Content != null)
            {
                await request.Content.ReadAsStringAsync()
                    .ContinueWith(task =>
                        {
                            requestLogEntry.ContentBody = task.Result;
                        }, 
                        cancellationToken
                    );
            }

            return await base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                    {
                        var response = task.Result;

                        requestLogEntry.ResponseStatusCode = (int) response.StatusCode;
                        requestLogEntry.DateTimeResponded = DateTime.Now;

                        if (response.Content != null)
                        {
                            requestLogEntry.ResponseContentBody = response.Content.ReadAsStringAsync().Result;
                            requestLogEntry.ResponseContentType = response.Content.Headers.ContentType.MediaType;
                            requestLogEntry.ResponseHeaders = SerializeHeaders(response.Content.Headers);
                        }

                        //if (!requestLogEntry.Method.Equals("OPTIONS"))
                        //{
                        //    requestLogsRepository.Add(requestLogEntry);
                        //    requestLogsRepository.UnitOfWork.Save();
                        //}

                        return response;
                    }, 
                    cancellationToken
                );
        }

        private static RequestLogEntity CreateApiLogEntryWithRequestData(HttpRequestMessage request)
        {
            var context = ((HttpContextBase)request.Properties["MS_HttpContext"]);
            
            return new RequestLogEntity
            {
                Key = GetKey(request.Headers),
                Token = GetToken(request.Headers),
                ContentType = context.Request.ContentType,
                IpAddress = context.Request.UserHostAddress,
                Method = request.Method.Method,
                Headers = SerializeHeaders(request.Headers),
                DateTimeRequested = DateTime.Now,
                Uri = request.RequestUri.PathAndQuery
            };
        }

        private static string SerializeHeaders(HttpHeaders headers)
        {
            var dict = new Dictionary<string, string>();

            foreach (var item in headers.ToList())
            {
                if (item.Value != null)
                {
                    var header = item.Value.Aggregate(String.Empty, (current, value) => current + (value + " "));
                    header = header.TrimEnd(" ".ToCharArray());

                    dict.Add(item.Key, header);
                }
            }

            return JsonConvert.SerializeObject(dict, Formatting.Indented);
        }

        private static string GetKey(HttpHeaders headers)
        {
            var key = default(string);

            if (headers.Contains("x-access-key"))
            {
                key = headers.GetValues("x-access-key").FirstOrDefault();
            }
            else if (headers.Contains("key"))
            {
                key = headers.GetValues("key").FirstOrDefault();
            }

            return key;
        }
        
        private static string GetToken(HttpHeaders headers)
        {
            var token = default(string);

            if (headers.Contains("x-access-token"))
            {
                token = headers.GetValues("x-access-token").FirstOrDefault();
            }
            else if (headers.Contains("token"))
            {
                token = headers.GetValues("token").FirstOrDefault();
            }

            try
            {
                var decryptedToken = string.IsNullOrEmpty(token) ? token : token.Decrypt();
                return decryptedToken;
            }
            catch (Exception)
            {
                return token;
            }                         
        }
    }
}