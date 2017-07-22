using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Microservice.Gateway.Services
{
    public class JsonSerializer : IJsonSerializer
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public string Serialize(object content)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            return JsonConvert.SerializeObject(content, Settings);
        }

        public T Deserialize<T>(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(content));

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
