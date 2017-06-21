using StoryLine.Wiremock.Actions;
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
                    .Performs<HttpResponseStub>(x => x
                        .Request()
                            .Path()
                                .EqualsTo("/xxx")
                            .Method("GET")
                        .Response()
                            .Status(200)
                            .Body("Text"))
                .Run();


            Config.ResetAll();
        }
    }
}
