using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DasonPokemon.Core.Entities;
using MongoDB.Driver;
using MongoDB.Extensions.Repository.Interfaces;

namespace DasonPokemon.Core.Services
{
    public class SetService : ISetService
    {
        private readonly ICardService _cardService;
        private readonly IMongoRepository<Set> _repository;

        private readonly Dictionary<string, List<string>> PackToSetsMapping;

        public SetService(ICardService cardService, IMongoRepository<Set> repository)
        {
            _cardService = cardService;
            _repository = repository;

            PackToSetsMapping = new Dictionary<string, List<string>> {
                { "Shining Fates", new List<string> { "swsh45", "swsh45sv" } },
                { "Vivid Voltage", new List<string> { "swsh4" } }
            };
        }

        public async Task<Set> GetAsync(Guid id) =>
            await _repository.GetAsync(id);

        public async Task<IEnumerable<Set>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<IEnumerable<Set>> GetSetsWithMatchingExternalIds(IEnumerable<string> externalIds)
        {
            var filter = Builders<Set>.Filter.In(c => c.ExternalId, externalIds);

            return await _repository.GetManyAsync(filter);
        }

        public async Task BulkUpsert(IEnumerable<Set> sets) =>
            await _repository.BulkUpsertAsync(sets, true);
    }
}
