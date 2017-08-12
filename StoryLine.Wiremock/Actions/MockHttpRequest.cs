using System;
using StoryLine.Contracts;
using StoryLine.Wiremock.Builders;

namespace StoryLine.Wiremock.Actions
{
    public class MockHttpRequest : IActionBuilder
    {
        private Action<RequestBuilder> _requestConfig = req => req.Method("GET").UrlPath("/");
        private Action<ResponseBuilder> _responseConfig = res => res.Body("Ok").Status(200);

        IAction IActionBuilder.Build()
        {
            var apiStubState = new ApiStubState();
            var requestBuilder = new RequestBuilder(apiStubState);
            _requestConfig(requestBuilder);

            var responseBuilder = new ResponseBuilder(apiStubState);
            _responseConfig(responseBuilder);

            return new MockHttpRequestAction(apiStubState);
        }

        public MockHttpRequest Request(Action<RequestBuilder> requestConfig)
        {
            _requestConfig = requestConfig ?? throw new ArgumentNullException(nameof(requestConfig));

            return this;
        }

        public MockHttpRequest Response(Action<ResponseBuilder> responseConfig)
        {
            _responseConfig = responseConfig ?? throw new ArgumentNullException(nameof(responseConfig));

            return this;
        }
    }
}
