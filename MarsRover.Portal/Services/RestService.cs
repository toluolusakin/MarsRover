using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MarsRover.Portal.Services
{
    public class RestService : IRestService
    {

        private readonly ILogger<RestService> _logger;

        public RestService(ILogger<RestService> logger)
        {
            _logger = logger;
        }

         /*private bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
         {
             return true;
         }*/
         
        private Task<IRestResponse> PostResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }

        public R PostRestAction<R, T>(T model, string url)
        {
            if (model == null) return default(R);
            var response = default(R);
            try
            {

                var json = JsonConvert.SerializeObject(model);
                IRestResponse restResponse = new RestResponse();
                var client = new RestClient(url);
                var webRequest = new RestRequest(Method.POST)
                {
                    RequestFormat = DataFormat.Json
                };

                webRequest.AddHeader("cache-control", "no-cache");
                webRequest.AddHeader("content-type", "application/json");
                webRequest.AddParameter("application/json", json, ParameterType.RequestBody);
                Task.Run(async () =>
                {
                    restResponse = await PostResponseContentAsync(client, webRequest) as RestResponse;
                }).Wait();

                if (restResponse.Content != null && restResponse.Content.Length > 0)
                {
                    response = JsonConvert.DeserializeObject<R>(restResponse.Content);
                }

                if (restResponse.StatusCode != HttpStatusCode.OK)
                {
                    _logger.LogWarning(restResponse.StatusCode + ".. .." + restResponse.StatusDescription + "..." + url);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on method PostRestAction" + ex.InnerException + ex.Message + ex.StackTrace);
            }

            return response;
        }

        public R GetRestAction<R>(string url)
        {
            var response = default(R);
            try
            {

                IRestResponse restResponse = new RestResponse();
                var client = new RestClient(url);
                var webRequest = new RestRequest(Method.GET);
                webRequest.AddHeader("content-type", "application/json");
                webRequest.AddHeader("cache-control", "no-cache");
                webRequest.AddHeader("content-type", "application/json");
                Task.Run(async () =>
                {
                    restResponse = await GetResponseContentAsync(client, webRequest) as RestResponse;
                }).Wait();

                if (restResponse.Content != null && restResponse.Content.Length > 0)
                {
                    response = JsonConvert.DeserializeObject<R>(restResponse.Content);
                }

                if (restResponse.StatusCode != HttpStatusCode.OK)
                {
                    _logger.LogWarning(restResponse.StatusCode + ".. .." + restResponse.StatusDescription + "..." + url);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on method GetRestAction" + ex.InnerException + ex.Message + ex.StackTrace);
            }

            return response;
        }

        private Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}
