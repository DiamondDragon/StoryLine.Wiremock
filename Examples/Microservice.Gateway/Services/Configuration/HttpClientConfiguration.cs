using System;

namespace Microservice.Gateway.Services.Configuration
{
    public class HttpClientConfiguration : IHttpClientConfiguration
    {
        public string BaseAddress { get; set; }
        public TimeSpan Timeout { get; set; }
    }
}