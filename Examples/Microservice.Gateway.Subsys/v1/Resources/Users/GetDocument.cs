using Microservice.Gateway.Subsys.v1.Models;
using StoryLine;
using StoryLine.Rest.Actions;
using StoryLine.Rest.Expectations;
using StoryLine.Rest.Expectations.Extensions;
using StoryLine.Wiremock.Actions;
using StoryLine.Wiremock.Builders;
using Xunit;

namespace Microservice.Gateway.Subsys.v1.Resources.Users
{
    public class GetDocument : ApiTestBase
    {
        private const int UserId = 555;
        private const int DocumentId = 444;
        private readonly Document _document;

        public GetDocument()
        {
            _document = new Document
            {
                Title = "Dragon1",
                Size = 12345
            };
        }

        [Fact]
        public void When_User_Not_Found_Service_Should_Return_404()
        {
            Scenario.New()
                .Given()
                .HasPerformed<MockHttpRequest>(x => x
                    .Request(req => req
                        .Method("GET")
                        .Url(p => p.EqualsTo(Config.ToDocumentServiceUrl($"/v2/documents/{DocumentId}"))))
                    .Response(res => res
                        .Status(404)))
                .When()
                .Performs<HttpRequest>(p => p
                    .Url($"/v1/users/{UserId}/documents/{DocumentId}"))
                .Then()
                .Expects<HttpResponse>(x => x
                    .Status(404))
                .Run();
        }

        [Fact]
        public void When_User_Found_Service_Should_Return_200_And_User_Content()
        {
            Scenario.New()
                .Given()
                .HasPerformed<MockHttpRequest>(x => x
                    .Request(req => req
                        .Method("GET")
                        .Url(p => p.EqualsTo(Config.ToDocumentServiceUrl($"/v2/documents/{DocumentId}"))))
                    .Response(res => res
                        .Status(200)
                        .JsonObjectBody(_document)))
                .When()
                .Performs<HttpRequest>(p => p
                    .Url($"/v1/users/{UserId}/documents/{DocumentId}"))
                .Then()
                .Expects<HttpResponse>(x => x
                    .Status(200)
                    .JsonBody()
                    .MatchesObject(_document))
                .Run();
        }

    }
}