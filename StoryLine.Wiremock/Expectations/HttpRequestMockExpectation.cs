using System;
using System.Threading;
using StoryLine.Contracts;
using StoryLine.Exceptions;
using StoryLine.Wiremock.Builders;

namespace StoryLine.Wiremock.Expectations
{
    public class HttpRequestMockExpectation : IExpectation
    {
        private readonly IApiStubState _state;
        private readonly int _retryCount;
        private readonly TimeSpan _retryInterval;

        public HttpRequestMockExpectation(
            ApiStubState state, 
            int retryCount, 
            TimeSpan retryInterval)
        {
            if (retryCount <= 0) throw new ArgumentOutOfRangeException(nameof(retryCount));

            _state = state ?? throw new ArgumentNullException(nameof(state));
            _retryCount = retryCount;
            _retryInterval = retryInterval;
        }

        public void Validate(IActor actor)
        {
            var count = WaitForMatchingRequestCount();

            if (!count)
                throw new ExpectationException($"Expected Api to be called '{_state.RequestCount.Description}' but was called '{count}'");
        }

        private bool WaitForMatchingRequestCount()
        {
            for (var i = -1; i < _retryCount; i++)
            {
                var count = Config.Client.Count(_state.RequestState);

                var hasMatchingRequestCount = _state.RequestCount.Evaluate(count);
                if (hasMatchingRequestCount)
                    return true;

                Thread.Sleep(_retryInterval);
            }

            return false;
        }
    }
}