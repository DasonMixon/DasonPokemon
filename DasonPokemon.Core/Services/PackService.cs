using DasonPokemon.Core.Entities;
using Microsoft.Extensions.Logging;
using MongoDB.Extensions.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DasonPokemon.Core.Services
{
    public class PackService : IPackService
    {
        private readonly IMongoRepository<Pack> _repository;
        private readonly ICardService _cardService;
        private readonly ILogger<PackService> _logger;

        public PackService(IMongoRepository<Pack> repository, ICardService cardService, ILogger<PackService> logger)
        {
            _repository = repository;
            _cardService = cardService;
            _logger = logger;
        }

        public async Task<IEnumerable<Card>> GetCardsFromPackSets(Guid id)
        {
            var pack = await GetPack(id);

            // From the pack details we can see what cards we need to grab from the list of sets
            var cards = new List<Card>();
            foreach (var set in pack.SetIds)
            {
                cards.AddRange(await _cardService.GetAllFromSetAsync(set));
            }

            return cards;
        }

        public async Task<Pack> GetPack(Guid id) =>
            await _repository.GetAsync(id);

        public async Task<IEnumerable<Pack>> GetAllPacks() =>
            await _repository.GetAllAsync();

        public async Task BulkUpsert(IEnumerable<Pack> packs) =>
            await _repository.BulkUpsertAsync(packs);

        public async Task<IEnumerable<Card>> GeneratePack(Guid id)
        {
            List<Card> results = new List<Card>();

            // TODO: This is an unnecessary call as the one below also grabs the pack from mongo, optimize all of this
            var pack = await GetPack(id);

            // First let's get all the cards we could possibly pull from this pack
            var possibleCards = await GetCardsFromPackSets(id);

            // Now lets create a list that honors the rarity rates
            var totalCards = new List<Card>();
            foreach (var card in possibleCards)
            {
                var rateFound = pack.RarityRates.TryGetValue(card.Rarity, out var pullRate);
                if (!rateFound)
                {
                    // If for some reason we don't have that rarity's pull rate recorded, log and skip the card
                    _logger.LogInformation($"Did not find recorded pull rate for pack '{pack.Id}', rarity '{card.Rarity}'. Skipping card '{card.Id}'");
                    continue;
                }

                // Based on the rate, add that many copies of the card to the totalCards list
                totalCards.AddRange(Enumerable.Repeat(card, (int)Math.Ceiling(pullRate)));
            }

            var commons = totalCards.Where(c => c.Rarity.Equals("Common", StringComparison.InvariantCultureIgnoreCase));
            var uncommons = totalCards.Where(c => c.Rarity.Equals("Uncommon", StringComparison.InvariantCultureIgnoreCase));
            var rares = totalCards.Except(commons).Except(uncommons).ToList();

            // Now we grab the cards for the pack opening. Start with the guaranteed rare
            var rareCard = rares.GetRandom(1);
            results.AddRange(rareCard);
            rares.Remove(rareCard.Single());

            // Roll 1/5 to see if they get a second rare
            var getSecondRare = PackExtensions.RollBetweenOneAnd(5);
            if (getSecondRare)
            {
                var secondRareCard = rares.GetRandom(1);
                results.AddRange(secondRareCard);
                rares.Remove(secondRareCard.Single());
            }

            // If they got a second rare card, they only get 2 uncommon cards. Otherwise they get 3
            var uncommonCards = uncommons.GetRandom(getSecondRare ? 2 : 3);
            results.AddRange(uncommonCards);

            // And then fill the rest with common cards
            var commonCards = commons.GetRandom(10 - results.Count);
            results.AddRange(commonCards);

            // And finally we want to order the cards properly
            var commonResults = results.Where(c => c.Rarity.Equals("Common", StringComparison.InvariantCultureIgnoreCase));
            var uncommonResults = results.Where(c => c.Rarity.Equals("Uncommon", StringComparison.InvariantCultureIgnoreCase));
            var rareResults = results.Except(commonResults).Except(uncommonResults);

            return commonResults.Concat(uncommonResults).Concat(rareResults);
        }
    }
}
