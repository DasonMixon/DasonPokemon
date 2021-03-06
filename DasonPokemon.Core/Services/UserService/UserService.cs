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
        private readonly ICardService _cardService;

        public UserService(IMongoRepository<User> repository, ICardService cardService)
        {
            _repository = repository;
            _cardService = cardService;
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

        public async Task UpdateCollection(CollectionServiceModel collection)
        {
            if (collection.AccountId == null || collection.AccountId == "")
                throw new ApplicationException("AccountId was not provided");

            var users = await _repository.GetManyAsync(u => u.PTCGOAccountId == collection.AccountId);
            if (users.Count() > 1)
                throw new ApplicationException($"Multiple users with same AccountId {collection.AccountId}");

            var user = users.SingleOrDefault();
            if (user == null)
            {
                throw new ApplicationException($"User with AccountId {collection.AccountId} was not found");
            }
            else
            {
                // This is where we need to build the user's collection based on the cards we have stored
                var cardIds = collection.Collection.Select(c => c.PTCGOCode).Distinct();
                var cards = await _cardService.GetCardsWithMatchingExternalIds(cardIds);

                user.Collection = cards.Select(c => new CollectionCard
                {
                    Card = c,
                    Count = collection.Collection.SingleOrDefault(i => i.PTCGOCode == i.PTCGOCode)?.Count ?? 0
                }).ToList();

                await _repository.ReplaceAsync(user);
            }
        }
    }
}
