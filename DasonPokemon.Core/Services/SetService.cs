using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DasonPokemon.Core.Entities;
using MongoDB.Driver;
using MongoDB.Extensions.Repository.Interfaces;

namespace DasonPokemon.Core.Services
{
    public class SetService : ISetService
    {
        private readonly IMongoRepository<Set> _repository;

        public SetService(IMongoRepository<Set> repository)
        {
            _repository = repository;
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
            await _repository.BulkUpsertAsync(sets);
    }
}
