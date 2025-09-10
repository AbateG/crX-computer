using System.Threading.Tasks;

namespace Infrastructure.ApiClients.Scheduling
{
    public class SchedulingApiClient
    {
        // TODO: Implement scheduling API integration methods
        public Task<string> GetScheduleAsync(string resourceId)
        {
            // Placeholder for external API call
            return Task.FromResult($"Schedule for {resourceId}");
        }
    }
}
