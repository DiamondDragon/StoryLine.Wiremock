using System;

namespace StoryLine.Wiremock.Builders
{
    public class UrlBuilder : StubBuilderBase
    {
        public UrlBuilder(IApiStubState state) 
            : base(state)
        {
        }

        public RequestBuilder EqualsTo(string url)
        {
            State.RequestState.Url = url ?? throw new ArgumentNullException(nameof(url));

            return new RequestBuilder(State);
        }

        public RequestBuilder Matches(string pattern)
        {
            State.RequestState.UrlPattern = pattern ?? throw new ArgumentNullException(nameof(pattern));

            return new RequestBuilder(State);
        }
    }
}