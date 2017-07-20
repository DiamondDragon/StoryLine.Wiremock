using System;
using System.Net.Http;
using System.Text;
using Microservice.Gateway.Services.Configuration;

namespace Microservice.Gateway.Services
{
    public class ServiceClient : IServiceClient
    {
        private readonly IHttpClientConfiguration _configuration;
        private readonly IJsonSerializer _serializer;

        public ServiceClient(IHttpClientConfiguration configuration, IJsonSerializer serializer)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public TResult Get<TResult>(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(url));

            using (var client = CreateClient())
            {
                var response = client.GetAsync(url).Result;

                return HandleResponse<TResult>(response);
            }
        }

        public TResult Post<TContent, TResult>(string url, TContent content)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(url));

            using (var client = CreateClient())
            {
                var response = client.PostAsync(
                    url, 
                    new StringContent(
                        _serializer.Serialize(content), 
                        Encoding.UTF8, 
                        "application/json")).Result;

                return HandleResponse<TResult>(response);
            }
        }

        private TResult HandleResponse<TResult>(HttpResponseMessage response)
        {
            return response.IsSuccessStatusCode ? _serializer.Deserialize<TResult>(response.Content.ReadAsStringAsync().Result) : default(TResult);
        }

        private HttpClient CreateClient()
        {
            return new HttpClient
            {
                BaseAddress = new Uri(_configuration.BaseAddress),
                Timeout = _configuration.Timeout,
            };
        }
    }
}
