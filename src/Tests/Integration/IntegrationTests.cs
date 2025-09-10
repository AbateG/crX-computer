using Xunit;

namespace Tests.Integration
{
    public class IntegrationTests
    {
        [Fact]
        public async Task HealthEndpoint_ReturnsSuccess()
        {
            // Example using HttpClient to check API health endpoint
            using var client = new System.Net.Http.HttpClient();
            var response = await client.GetAsync("http://localhost:5000/health");
            response.EnsureSuccessStatusCode();
        }
    }
}
