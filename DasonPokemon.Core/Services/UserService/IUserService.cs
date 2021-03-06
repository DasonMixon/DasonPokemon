using System.Threading.Tasks;
using DasonPokemon.Core.Models;

namespace DasonPokemon.Core.Services.UserService
{
    public interface IUserService
    {
        Task<LinkUserResult> LinkAccount(LinkAccountServiceModel link);
    }
}
