using Xunit;

namespace Microservice.Gateway.Subsys
{
    [CollectionDefinition(nameof(Config))]
    public class ConfigCollection : ICollectionFixture<Config>
    {
    }
}