namespace Microservice.Gateway.Services
{
    public interface IJsonSerializer
    {
        string Serialize(object content);
        T Deserialize<T>(string content);
    }
}