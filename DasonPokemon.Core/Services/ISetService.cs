using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DasonPokemon.Core.Entities;

namespace DasonPokemon.Core.Services
{
    public interface ISetService
    {
        Task<Set> GetAsync(Guid id);
        Task<IEnumerable<Set>> GetAllAsync();
        Task BulkUpsert(IEnumerable<Set> sets);
        Task<IEnumerable<Set>> GetSetsWithMatchingExternalIds(IEnumerable<string> externalIds);
        Task<Pack> GeneratePack(Guid setId);
    }
}
