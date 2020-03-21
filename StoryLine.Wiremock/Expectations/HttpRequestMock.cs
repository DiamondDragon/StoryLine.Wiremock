using System;
using StoryLine.Contracts;
using StoryLine.Wiremock.Builders;

namespace StoryLine.Wiremock.Expectations
{
    public class HttpRequestMock : IExpectationBuilder
    {
        private Action<RequestBuilder> _requestConfig = req => req.Method("GET").UrlPath("/");
        private Action<RequestCountBuilder> _requestCountConfig = c => c.AtLeastOnce();
        private int _retryCount;
        private TimeSpan _retryInterval = TimeSpan.FromMilliseconds(100);

        public HttpRequestMock Request(Action<RequestBuilder> requestConfig)
        {
            _requestConfig = requestConfig ?? throw new ArgumentNullException(nameof(requestConfig));

            return this;
        }

        public HttpRequestMock Called(Action<RequestCountBuilder> requestCountConfig)
        {
            _requestCountConfig = requestCountConfig ?? throw new ArgumentNullException(nameof(requestCountConfig));

            return this;
        }

        public HttpRequestMock WithRetries(int retryCount)
        {
            if (retryCount <= 0) 
                throw new ArgumentOutOfRangeException(nameof(retryCount));

            _retryCount = retryCount;

            return this;
        }

        public HttpRequestMock WithRetryInterval(TimeSpan interval)
        {
            _retryInterval = interval;

            return this;
        }

        IExpectation IExpectationBuilder.Build()
        {
            var state = new ApiStubState();
            var requestBuilder = new RequestBuilder(state);
            _requestConfig(requestBuilder);

            var requestCountBuilder = new RequestCountBuilder(state);
            _requestCountConfig(requestCountBuilder);

            return new HttpRequestMockExpectation(
                state,
                _retryCount,
                _retryInterval
                );
        }
    }
}
