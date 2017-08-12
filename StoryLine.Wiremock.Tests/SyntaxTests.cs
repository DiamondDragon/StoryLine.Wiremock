using StoryLine.Wiremock.Actions;
using StoryLine.Wiremock.Expectations;

namespace StoryLine.Wiremock.Tests
{
    public class SyntaxTests
    {
        //[Fact]
        public void Test()
        {
            Config.SetBaseAddress("http://localhost:32769");

            Scenario.New()
                .When()
                    .Performs<MockHttpRequest>(x => x
                        .Request(req => req
                            .Url("/xxx")
                            .Method("GET"))
                        .Response(res => res
                            .Status(200)
                            .Body("Text")))
                .Then()
                    .Expects<HttpRequestMock>(x => x
                        .Request(req => req
                            .Url("/dragon"))
                        .Called(c => c.AtLeastOnce()))
                .Run();


            Config.ResetAll();
        }
    }
}
