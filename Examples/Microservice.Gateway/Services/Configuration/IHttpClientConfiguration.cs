using System;

namespace Microservice.Gateway.Services.Configuration
{
    public interface IHttpClientConfiguration
    {
        string BaseAddress { get; }
        TimeSpan Timeout { get; }
    }
}