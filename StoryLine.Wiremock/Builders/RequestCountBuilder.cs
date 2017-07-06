using System;
using StoryLine.Wiremock.Services;

namespace StoryLine.Wiremock.Builders
{
    public class RequestCountBuilder
    {
        private readonly IApiStubState _state;

        public RequestCountBuilder(IApiStubState state)
        {
            _state = state ?? throw new ArgumentNullException(nameof(state));
        }

        public RequestCountBuilder Never()
        {
            return SetRequestCount(count => count == 0, "never");
        }
        public RequestCountBuilder Once()
        {
            return SetRequestCount(count => count == 1, "once");
        }

        public RequestCountBuilder Twice()
        {
            return SetRequestCount(count => count == 2, "twice");
        }

        public RequestCountBuilder AtLeastOnce()
        {
            return SetRequestCount(count => count >= 1, "at least once");
        }

        public RequestCountBuilder MoreThanOnce()
        {
            return SetRequestCount(count => count > 1, "more than once");
        }

        public RequestCountBuilder Exactly(int number)
        {
            if (number <= 0)
                throw new ArgumentOutOfRangeException(nameof(number));

            return SetRequestCount(count => count == number, string.Format("{0} times", number));
        }

        private RequestCountBuilder SetRequestCount(Predicate<int> predicate, string description)
        {
            _state.RequestCount = new Times(predicate, description);

            return this;
        }
    }
}