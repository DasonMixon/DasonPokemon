using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DasonPokemon.Core.Entities;

namespace DasonPokemon.Core.Services
{
    public interface ICardService
    {
        Task<Card> GetAsync(Guid id);
        Task<IEnumerable<Card>> GetAllFromSetAsync(Guid setId);
        Task BulkUpsert(IEnumerable<Card> cards);
        Task<IEnumerable<Card>> GetCardsWithMatchingExternalIds(IEnumerable<string> externalIds);
    }
}
