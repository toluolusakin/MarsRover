using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Portal.Services
{
    public interface IRestService
    {
        //bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors);
        R PostRestAction<R, T>(T model, string url);
        R GetRestAction<R>(string url);
    }
}
