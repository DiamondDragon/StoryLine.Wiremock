using System;
using Xunit;

namespace Microservice.Gateway.Subsys
{
    [Collection(nameof(Config))]
    public abstract class ApiTestBase : IDisposable
    {
        protected ApiTestBase()
        {
            StoryLine.Wiremock.Config.ResetAll();
        }

        public virtual void Dispose()
        {
            StoryLine.Wiremock.Config.ResetAll();
        }
    }
}