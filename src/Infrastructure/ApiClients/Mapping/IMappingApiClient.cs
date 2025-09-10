using System.Threading.Tasks;

namespace Infrastructure.ApiClients.Mapping
{
    public interface IMappingApiClient
    {
        Task<string> GetMapDataAsync(string location);
    }
}
