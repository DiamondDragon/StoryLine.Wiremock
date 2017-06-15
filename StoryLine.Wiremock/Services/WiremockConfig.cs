using System;

namespace StoryLine.Wiremock.Services
{
    public class WiremockConfig : IWiremockConfig
    {
        private string _serverAddress;

        public string ServerAddress
        {
            get => _serverAddress;
            set => _serverAddress = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}