using System;
using System.Linq;
using System.Threading.Tasks;
using DasonPokemon.Core.Entities;
using DasonPokemon.Core.Models;
using MongoDB.Extensions.Repository.Interfaces;

namespace DasonPokemon.Core.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMongoRepository<User> _repository;

        public UserService(IMongoRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<LinkUserResult> LinkAccount(LinkAccountServiceModel link)
        {
            if (link.Email == null || link.Email == "")
                return new LinkUserResult { WasSuccessful = false, FailureReason = "Email cannot be null or empty." };

            if (link.AccountId == null || link.AccountId == "")
                return new LinkUserResult { WasSuccessful = false, FailureReason = "AccountId cannot be null or empty." };

            var existingUser = (await _repository.GetManyAsync(u => u.Email == link.Email)).SingleOrDefault();
            if (existingUser == null)
            {
                // Create the user
                var newUser = new User { Id = Guid.NewGuid(), Email = link.Email, PTCGOAccountId = link.AccountId };
                await _repository.AddAsync(newUser);
            } else
            {
                // Update the user
                existingUser.PTCGOAccountId = link.AccountId;
                await _repository.ReplaceAsync(existingUser);
            }

            return new LinkUserResult { WasSuccessful = true };
        }
    }
}
