using StoryLine.Wiremock.Services;
using StoryLine.Wiremock.Services.Contracts;

namespace StoryLine.Wiremock.Builders
{
    public interface IApiStubState
    {
        Request RequestState { get; }
        Response ResponseState { get; }
        ITimes RequestCount { get; set; }
    }
}