using System.Threading.Tasks;
using DasonPokemon.Core.Models;

namespace DasonPokemon.Core.Services.UserService
{
    public interface IUserService
    {
        Task<CreateUserResult> CreateUser(UserServiceModel user);
        Task<AuthenticateUserResult> Authenticate(string email, string password);
        Task<AuthorizeUserResult> Authorize(string accessToken, string refreshToken);
    }
}
