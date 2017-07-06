using StoryLine.Wiremock.Actions;
using StoryLine.Wiremock.Expectations;
using Xunit;

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
                        .Request()
                            .Path()
                                .EqualsTo("/xxx")
                            .Method("GET")
                        .Response()
                            .Status(200)
                            .Body("Text")
                        )
                .Then()
                    .Expects<HttpRequestMock>(x => x
                        .Request()
                            .Path(p => p.EqualsTo("/dragon"))
                        .Called()
                            .AtLeastOnce())
                .Run();


            Config.ResetAll();
        }
    }
}
