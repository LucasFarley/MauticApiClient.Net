using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MauticApiClient.Net
{
    public class HttpClientProvider : IHttpClientProvider
    {
        private readonly string _baseUrl;
        private readonly string _username;
        private readonly string _password;
        private readonly IWebProxy _WebProxy;

        public HttpClientProvider(string baseUrl, string username, string password, IWebProxy pWebProxy = null){
            _baseUrl = baseUrl;
            _username = username;
            _password = password;
            _WebProxy = pWebProxy;
        }

        public HttpClient GetHttpClient(){

            var handler = new HttpClientHandler();
            if (_WebProxy == null){
                handler.UseDefaultCredentials = true;
            } else {
                handler.Proxy = _WebProxy;
            }
            var client = new HttpClient(handler);

            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var byteArray = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", _username, _password));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            ServicePointManager.ServerCertificateValidationCallback += NotValidServerCertficate;

            return client;
        }
        public static bool NotValidServerCertficate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
        {
            return true;
        }
    }
}
