using MarsRover.Portal.Models;
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
    public class MarsRoverService : IMarsRoverService
    {
        public static IConfiguration Configuration { get; set; }
        private readonly IRestService _restService;
        public MarsRoverService(IRestService restService)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? ""}.json");
            Configuration = builder.Build();
            _restService = restService;
        }

       
        public MarsRoverResponse MoverRover(MarsRoverRequest payload)
        {
            string url = Configuration["BaseUrl"] + "/MoveRovers";
            return _restService.PostRestAction<MarsRoverResponse, MarsRoverRequest>(payload, url);
        }

        public MarsRoverHistoryResponse GetHistory()
        {
            string url = Configuration["BaseUrl"] + "/GetHistory";
            return _restService.GetRestAction<MarsRoverHistoryResponse>(url);
        }

        /*public MarsRoverResponse MoverRover(MarsRoverRequest payload)
        {
            string url = Configuration["BaseUrl"] + "/MoveRovers";
            return _restService.PostRestAction<MarsRoverResponse, MarsRoverRequest>(payload, url);
        }
        */
    }
}
