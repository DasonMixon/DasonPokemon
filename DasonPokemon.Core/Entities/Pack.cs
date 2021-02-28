using DasonPokemon.Core.Entities;
using MongoDB.Extensions.Repository.Models;
using System;
using System.Collections.Generic;

namespace DasonPokemon.Core
{
    public class Pack : MongoEntity
    {
        public string Name { get; set; }
        // Certain packs contains cards from multiple sets, ex: Shining Fates
        public IEnumerable<Guid> SetIds { get; set; }
        public IEnumerable<string> ExternalSetIds { get; set; }
        // public IEnumerable<Card> Cards { get; set; } TODO: Make a PackOpening class or something that has list of cards that were pulled for the pack, no need to store this
        public Dictionary<string, double> RarityRates { get; set; }
        public string Image { get; set; }
    }
}
