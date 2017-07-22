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
    public class GetUserDocuments : ApiTestBase
    {
        private const string UserId = "3333";
        private readonly DocumentCollection _documents;

        public GetUserDocuments()
        {
            _documents = new DocumentCollection
            {
                Items = new[]
                {
                    new Document
                    {
                        Title = "doc1",
                        Size = 1000
                    },
                    new Document
                    {
                        Title = "doc2",
                        Size = 2000
                    }
                }
            };
        }

        [Fact]
        public void When_Paging_Parameters_Providers_Provided_Should_Use_Default_Values_And_Return_Result_Of_User_Service()
        {
            Scenario.New()
                .Given()
                .HasPerformed<MockHttpRequest>(x => x
                    .Request(req => req
                        .Method("GET")
                        .Path(p => p.EqualsTo(Config.ToDocumentServiceUrl("/v2/documents"))
                            .QueryParam("skip", "10")
                            .QueryParam("take", "15")
                            .QueryParam("user", UserId)))
                    .Response(res => res
                        .Status(200)
                        .JsonObjectBody(_documents)))
                .When()
                .Performs<HttpRequest>(p => p
                    .Url($"/v1/users/{UserId}/documents?take=15&skip=10"))
                .Then()
                .Expects<HttpResponse>(x => x
                    .Status(200)
                    .JsonBody()
                    .MatchesObject(_documents))
                .Run();
        }

        [Fact]
        public void When_No_Paging_Parameters_Provided_Should_Use_Default_Values_And_Return_Result_Of_User_Service()
        {
            Scenario.New()
                .Given()
                .HasPerformed<MockHttpRequest>(x => x
                    .Request(req => req
                        .Method("GET")
                        .Path(p => p.EqualsTo(Config.ToDocumentServiceUrl("/v2/documents"))
                            .QueryParam("skip", "0")
                            .QueryParam("take", "25")
                            .QueryParam("user", UserId)))
                    .Response(res => res
                        .Status(200)
                        .JsonObjectBody(_documents)))
                .When()
                .Performs<HttpRequest>(p => p
                    .Url($"/v1/users/{UserId}/documents"))
                .Then()
                .Expects<HttpResponse>(x => x
                    .Status(200)
                    .JsonBody()
                    .MatchesObject(_documents))
                .Run();
        }
    }
}