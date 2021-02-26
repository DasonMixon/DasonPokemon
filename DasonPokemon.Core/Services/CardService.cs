using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DasonPokemon.Core.Entities;
using MongoDB.Driver;
using MongoDB.Extensions.Repository.Interfaces;

namespace DasonPokemon.Core.Services
{
    public class CardService : ICardService
    {
        private readonly IMongoRepository<Card> _repository;

        public CardService(IMongoRepository<Card> repository)
        {
            _repository = repository;
        }

        public async Task<Card> GetAsync(Guid id) =>
            await _repository.GetAsync(id);


        public async Task<IEnumerable<Card>> GetCardsWithMatchingExternalIds(IEnumerable<string> externalIds)
        {
            var filter = Builders<Card>.Filter.In(c => c.ExternalId, externalIds);

            return await _repository.GetManyAsync(filter);
        }

        public async Task<IEnumerable<Card>> GetAllFromSetAsync(Guid setId) =>
            await _repository.GetManyAsync(c => c.Set.Id == setId);

        public async Task BulkUpsert(IEnumerable<Card> cards) =>
            await _repository.BulkUpsertAsync(cards);
    }
}
