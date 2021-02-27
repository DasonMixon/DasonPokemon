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

        public SetService(ICardService cardService, IMongoRepository<Set> repository)
        {
            _cardService = cardService;
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

        public async Task<Pack> GeneratePack(Guid setId)
        {
            // 1 Rare or better guaranteed (sometimes 2)
            // Generally 3 uncommon cards
            // Rest are common

            var setCards = await _cardService.GetAllFromSetAsync(setId);

            var commonCards = setCards.Where(sc => sc.Rarity == "Common");
            var uncommonCards = setCards.Where(sc => sc.Rarity == "Uncommon");

            // Rare              1/10
            // Rare Holo         1/20
            // Rare Holo EX
            // Rare Holo GX
            // Rare Holo V       1/25
            // Rare Holo VMAX    1/20
            // Rare Ultra        1/50
            // Amazing Rare      1/60
            // Rare Rainbow      1/100
            // Rare Secret
            // Rare Shining
            // Rare Shiny
            // Rare Shiny GX



            /*
             "Amazing Rare",
      "Common",
      "LEGEND",
      "Promo",
      "Rare",
      "Rare ACE",
      "Rare BREAK",
      "Rare Holo",
      "Rare Holo EX",
      "Rare Holo GX",
      "Rare Holo LV.X",
      "Rare Holo Star",
      "Rare Holo V",
      "Rare Holo VMAX",
      "Rare Prime",
      "Rare Prism Star",
      "Rare Rainbow",
      "Rare Secret",
      "Rare Shining",
      "Rare Shiny",
      "Rare Shiny GX",
      "Rare Ultra",
      "Uncommon"
             */
            var rareCards = setCards.Except(commonCards).Except(uncommonCards);
            

            var rarities = setCards.Select(sc => sc.Rarity).Distinct();
            var supertypes = setCards.Select(sc => sc.Supertype).Distinct();
            var subtypes = setCards.Select(sc => sc.Subtypes).SelectMany(c => c).Distinct();
            return null;
        }
    }
}
