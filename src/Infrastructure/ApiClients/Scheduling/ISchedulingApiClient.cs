using System.Threading.Tasks;

namespace Infrastructure.ApiClients.Scheduling
{
    public interface ISchedulingApiClient
    {
        Task<string> GetScheduleAsync(string resourceId);
    }
}
