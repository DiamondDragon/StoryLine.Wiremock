namespace Microservice.Gateway.Services.Configuration
{
    public interface IHttpClientConfigurationService
    {
        IHttpClientConfiguration Get(string serviceName);
    }
}