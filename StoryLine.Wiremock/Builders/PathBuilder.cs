using System;

namespace StoryLine.Wiremock.Builders
{
    public class PathBuilder : StubBuilderBase
    {
        public PathBuilder(IApiStubState state) 
            : base(state)
        {
        }

        public RequestBuilder EqualsTo(string path)
        {
            State.RequestState.UrlPath = path ?? throw new ArgumentNullException(nameof(path));

            return new RequestBuilder(State);
        }
        public RequestBuilder Matches(string pattern)
        {
            State.RequestState.UrlPathPattern = pattern ?? throw new ArgumentNullException(nameof(pattern));

            return new RequestBuilder(State);
        }
    }
}