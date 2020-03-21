using System;
using FakeItEasy;
using StoryLine.Wiremock.Expectations;
using StoryLine.Wiremock.Services;
using StoryLine.Wiremock.Services.Contracts;
using Xunit;
using Times = FakeItEasy.Times;

namespace StoryLine.Wiremock.Tests.Expectations
{
    public class HttpRequestMockTests : IDisposable
    {
        private readonly IWiremockClient _prevWireMockClient;
        private readonly IWiremockClient _wiremockClient;

        public HttpRequestMockTests()
        {
            _prevWireMockClient = Config.Client;

            _wiremockClient = A.Fake<IWiremockClient>();

            Config.Client = _wiremockClient;

            A.CallTo(() => _wiremockClient.Count(A<Request>._)).Returns(1);
        }

        [Fact]
        public void Valide_When_Created_With_Default_Settings_Client_And_Count_Matches_Predicate_Should_Call_Client_Once()
        {
            Scenario.New()
                .When()
                .Then()
                .Expects<HttpRequestMock>(x => x.Called(p => p.Once()))
                .Run();

            A.CallTo(() => _wiremockClient.Count(A<Request>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Valide_When_Created_With_Default_Settings_Client_And_Count_Not_Matches_Predicate_Should_Call_Client_Once()
        {
            A.CallTo(() => _wiremockClient.Count(A<Request>._)).Returns(0);

            Assert.Throws<Exceptions.ExpectationException> (() =>
            {
                Scenario.New()
                    .When()
                    .Then()
                    .Expects<HttpRequestMock>(x => x.Called(p => p.Once()))
                    .Run();
            });


            A.CallTo(() => _wiremockClient.Count(A<Request>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Valide_When_Created_With_Specific_Retry_Count_Client_And_Count_Not_Matches_Predicate_Should_Call_Client_Requested_Amount_Of_Times()
        {
            A.CallTo(() => _wiremockClient.Count(A<Request>._)).Returns(0);

            Assert.Throws<Exceptions.ExpectationException>(() =>
            {
                Scenario.New()
                    .When()
                    .Then()
                    .Expects<HttpRequestMock>(x => x
                        .Retries(5)
                        .Called(p => p.Once()))
                    .Run();
            });

            A.CallTo(() => _wiremockClient.Count(A<Request>._)).MustHaveHappened(6, Times.Exactly);
        }

        [Fact]
        public void Valide_When_Created_With_Specific_Retry_Count_Client_And_Count_Matches_During_The_First_Call_Should_Call_Client_Once()
        {
            A.CallTo(() => _wiremockClient.Count(A<Request>._)).Returns(1);

            Scenario.New()
                .When()
                .Then()
                .Expects<HttpRequestMock>(x => x
                    .Retries(5)
                    .Called(p => p.Once()))
                .Run();

            A.CallTo(() => _wiremockClient.Count(A<Request>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Valide_When_Created_With_Specific_Retry_Count_Client_And_Count_Matches_During_Next_Calls_Should_Call_Client_Multiple()
        {
            A.CallTo(() => _wiremockClient.Count(A<Request>._)).ReturnsNextFromSequence(0, 0, 0, 1);

            Scenario.New()
                .When()
                .Then()
                .Expects<HttpRequestMock>(x => x
                    .Retries(5)
                    .Called(p => p.Once()))
                .Run();

            A.CallTo(() => _wiremockClient.Count(A<Request>._)).MustHaveHappened(4, Times.Exactly);
        }

        public void Dispose()
        {
            Config.Client = _prevWireMockClient;
        }
    }
}
