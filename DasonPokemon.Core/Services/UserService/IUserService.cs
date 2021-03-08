using System;
using System.Threading.Tasks;
using DasonPokemon.Core.Entities;
using DasonPokemon.Core.Models;

namespace DasonPokemon.Core.Services.UserService
{
    public interface IUserService
    {
        Task<LinkUserResult> LinkAccount(LinkAccountServiceModel link);
        Task UpdateCollection(CollectionServiceModel collection);
        Task<User> GetUser(Guid id);
        Task<User> GetUser(string email);
        Task<User> Create(UserServiceModel user);
    }
}
