using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Microservice.Gateway.Subsys
{
    public class Config
    {
        public Config()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("settings.json")
                .Build();

            StoryLine.Rest.Config.AddServiceEndpont("Gateway", config["ServiceAddress"]);
            StoryLine.Rest.Config.SetAssemblies(typeof(Config).GetTypeInfo().Assembly);

            StoryLine.Wiremock.Config.SetBaseAddress(config["WireMockAddress"]);
        }

        public static string ToUserServiceUrl(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(path));

            return "/users" + path;
        }

        public static string ToDocumentServiceUrl(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(path));

            return "/documents" + path;
        }
    }
}
