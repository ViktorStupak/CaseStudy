using CaseStudy.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CaseStudy.Test.IntegrationTests
{
    public class TestControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public TestControllerTests(WebApplicationFactory<Startup> factory)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            _factory = factory;
        }

        [Theory]
        [InlineData("/swagger")]
        [InlineData("/Test/ping")]
        [InlineData("/Test/dbPing")]
        public async Task TestBasicCall(string endpoint)
        {
            // Arrange
            var client = _factory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5001");
            // Act
            var response = await client.GetAsync(new Uri(endpoint, UriKind.Relative)).ConfigureAwait(false);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }
    }
}