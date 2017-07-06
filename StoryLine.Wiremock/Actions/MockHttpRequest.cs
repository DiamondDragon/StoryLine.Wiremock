using StoryLine.Contracts;
using StoryLine.Wiremock.Builders;

namespace StoryLine.Wiremock.Actions
{
    public class MockHttpRequest : IActionBuilder
    {
        private readonly IApiStubState _apiStubState = new ApiStubState();

        IAction IActionBuilder.Build()
        {
            return new MockHttpRequestAction(_apiStubState);
        }

        public RequestBuilder Request()
        {
            return new RequestBuilder(_apiStubState);
        }
    }
}
