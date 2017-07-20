using System;
using Microservice.Gateway.Services.Configuration;

namespace Microservice.Gateway.Services
{
    public class ServiceClientFactory : IServiceClientFactory
    {
        private readonly IHttpClientConfigurationService _configurationService;
        private readonly IJsonSerializer _serializer;

        public ServiceClientFactory(
            IHttpClientConfigurationService configurationService,
            IJsonSerializer serializer)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public IServiceClient Create(string serviceName)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(serviceName));

            var configuration = _configurationService.Get(serviceName);
            if (configuration == null)
                throw new ArgumentException($"Unknown service \"{serviceName}\" was requested.");

            return new ServiceClient(configuration, _serializer);
        }
    }
}