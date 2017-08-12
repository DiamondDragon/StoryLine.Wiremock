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
    public class GetAllUser : ApiTestBase
    {
        private readonly UserCollection _users;

        public GetAllUser()
        {
            _users = new UserCollection
            {
                Items = new[]
                {
                    new User
                    {
                        Id = 1,
                        FirstName = "Diamond",
                        LastName = "Dragon",
                        Age = 33
                    },
                    new User
                    {
                        Id = 2,
                        FirstName = "Silver",
                        LastName = "Eagle",
                        Age = 44
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
                        .UrlPath(Config.ToUserServiceUrl("/users"))
                            .QueryParam("skip", "10")
                            .QueryParam("take", "15"))
                    .Response(res => res
                        .Status(200)
                        .JsonObjectBody(_users)))
                .When()
                    .Performs<HttpRequest>(p => p
                        .Url("/v1/users?take=15&skip=10"))
                .Then()
                    .Expects<HttpResponse>(x => x
                        .Status(200)
                        .JsonBody()
                        .MatchesObject(_users))
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
                        .UrlPath(Config.ToUserServiceUrl("/users"))
                        .QueryParam("skip", "0")
                        .QueryParam("take", "25"))
                    .Response(res => res
                        .Status(200)
                        .JsonObjectBody(_users)))
                .When()
                    .Performs<HttpRequest>(p => p
                        .Url("/v1/users"))
                .Then()
                    .Expects<HttpResponse>(x => x
                        .Status(200)
                        .JsonBody()
                        .MatchesObject(_users))
                .Run();
        }

    }
}