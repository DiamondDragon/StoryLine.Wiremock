using System;

namespace StoryLine.Wiremock.Builders
{
    public class RequestBuilder : StubBuilderBase
    {
        public RequestBuilder(IApiStubState state) 
            : base(state)
        {
        }

        public RequestBuilder Method(string method)
        {
            if (string.IsNullOrWhiteSpace(method))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(method));

            State.RequestState.Method = method.ToUpper();

            return this;
        }

        public RequestBuilder Url(string url)
        {
            State.RequestState.Url = url ?? throw new ArgumentNullException(nameof(url));

            return this;
        }

        public RequestBuilder UrlPattern(string urlPattern)
        {
            State.RequestState.UrlPattern = urlPattern ?? throw new ArgumentNullException(nameof(urlPattern));

            return this;
        }

        public RequestBuilder UrlPath(string path)
        {
            State.RequestState.UrlPath = path ?? throw new ArgumentNullException(nameof(path));

            return this;
        }

        public RequestBuilder UrlPathPattern(string urlPathPattern)
        {
            State.RequestState.UrlPathPattern = urlPathPattern ?? throw new ArgumentNullException(nameof(urlPathPattern));

            return this;
        }


        public HeaderBuilder Header(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(key));

            return new HeaderBuilder(State, key);
        }

        public RequestBuilder Header(string key, string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(key));

            var builder = new HeaderBuilder(State, key);
            return builder.EqualsTo(value);
        }

        public BodyBuilder Body()
        {
            return new BodyBuilder(State);
        }

        public RequestBuilder Body(string body)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            var builder = new BodyBuilder(State);
            return builder.EqualTo(body);
        }

        public QueryParamBuilder QueryParam(string key)
        {
            return new QueryParamBuilder(State, key);
        }

        public RequestBuilder QueryParam(string key, string value)
        {
            var builder = new QueryParamBuilder(State, key);
            return builder.EqualsTo(value);
        }
    }
}