using System.Threading.Tasks;

namespace Infrastructure.ApiClients.Mapping
{
    public class MappingApiClient
    {
        // TODO: Implement mapping API integration methods
        public Task<string> GetMapDataAsync(string location)
        {
            // Placeholder for external API call
            return Task.FromResult($"Map data for {location}");
        }
    }
}
