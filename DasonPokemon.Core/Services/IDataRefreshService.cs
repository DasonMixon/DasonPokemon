using System.Threading.Tasks;

namespace DasonPokemon.Core.Services
{
    public interface IDataRefreshService
    {
        Task<bool> Refresh(string baseUrl, string apiKey);
    }
}
