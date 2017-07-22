namespace Microservice.Gateway.Services
{
    public interface IServiceClientFactory
    {
        IServiceClient Create(string serviceName);
    }
}