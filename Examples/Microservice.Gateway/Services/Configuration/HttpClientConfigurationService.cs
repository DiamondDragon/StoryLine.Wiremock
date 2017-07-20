using System;
using Microsoft.Extensions.Options;

namespace Microservice.Gateway.Services.Configuration
{
    public class HttpClientConfigurationService : IHttpClientConfigurationService
    {
        private readonly IOptions<ServiceEndpointsSection> _endpointsSection;

        public HttpClientConfigurationService(IOptions<ServiceEndpointsSection> endpointsSection)
        {
            _endpointsSection = endpointsSection ?? throw new ArgumentNullException(nameof(endpointsSection));
        }

        public IHttpClientConfiguration Get(string serviceName)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
                throw new ArgumentNullException(nameof(serviceName));

            return _endpointsSection.Value.ContainsKey(serviceName) ? 
                _endpointsSection.Value[serviceName] : 
                null;
        }
    }
}