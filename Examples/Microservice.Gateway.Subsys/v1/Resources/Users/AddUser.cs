using Microservice.Gateway.Subsys.v1.Models;
using StoryLine;
using StoryLine.Rest.Actions;
using StoryLine.Rest.Actions.Extensions;
using StoryLine.Rest.Expectations;
using StoryLine.Rest.Expectations.Extensions;
using StoryLine.Wiremock.Actions;
using StoryLine.Wiremock.Builders;
using Xunit;

namespace Microservice.Gateway.Subsys.v1.Resources.Users
{
    public class AddUser : ApiTestBase
    {
        private readonly User _request;
        private readonly User _response;

        public AddUser()
        {
            _request = new User
            {
                FirstName = "Diamond",
                LastName = "Dragon",
                Age = 23
            };

            _response = new User
            {
                Id = 22,
                FirstName = "Diamond1",
                LastName = "Dragon1",
                Age = 24
            };
        }

        [Fact]
        public void When_New_User_Created_Should_Return_201_And_Valid_Location_Header()
        {
            Scenario.New()
                .Given()
                .HasPerformed<MockHttpRequest>(x => x
                    .Request(req => req
                        .Method("POST")
                        .Path(p => p.EqualsTo(Config.ToUserServiceUrl("/users")))
                        .Body()
                        .EqualToJsonObjectBody(_request, false))
                    .Response(res => res
                        .Status(200)
                        .JsonObjectBody(_response)))
                .When()
                    .Performs<HttpRequest>(p => p
                        .Method("POST")
                        .Header("Content-Type", "application/json")
                        .Url("/v1/users")
                        .JsonObjectBody(_request))
                .Then()
                .Expects<HttpResponse>(x => x
                    .Status(201)
                    .Header("Location")
                        .Contains("v1/users/")
                    .JsonBody()
                        .MatchesObject(_response))
                .Run();
        }
    }
}