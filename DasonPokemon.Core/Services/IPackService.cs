using DasonPokemon.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DasonPokemon.Core.Services
{
    public interface IPackService
    {
        Task<Pack> GetPack(Guid id);
        Task<IEnumerable<Pack>> GetAllPacks();
        Task<IEnumerable<Card>> GetCardsFromPackSets(Guid id);
        Task BulkUpsert(IEnumerable<Pack> packs);
        Task<IEnumerable<Card>> GeneratePack(Guid id);
    }
}
