using System;
using System.Collections.Generic;
using MongoDB.Extensions.Repository.Models;

namespace DasonPokemon.Core.Entities
{
    public class Card : MongoEntity
    {
        public string Name { get; set; }

        public string Supertype { get; set; }

        public List<string> Subtypes { get; set; }

        public string Hitpoints { get; set; }

        public List<string> Types { get; set; }

        public string EvolvesFrom { get; set; }

        public List<Ability> Abilities { get; set; }

        public List<Attack> Attacks { get; set; }

        public List<Weakness> Weaknesses { get; set; }

        public List<Resistance> Resistances { get; set; }

        public List<string> RetreatCost { get; set; }

        public int ConvertedRetreatCost { get; set; }

        public Set Set { get; set; }

        public string Number { get; set; }

        public string Artist { get; set; }

        public string Rarity { get; set; }

        public string FlavorText { get; set; }

        public List<int> NationalPokedexNumbers { get; set; }

        public Dictionary<string, string> Legalities { get; set; } // Unlimited, Expanded, etc..

        public Dictionary<string, string> Images { get; set; } // Symbol, Logo, etc.. (Our custom pack image should be included in here)

        public TCGPlayer TCGPlayer { get; set; }

        public string GetDeckListEntry(int count)
        {
            return $"* {count} {Name} {Set.PTCGOCode} {Number}";
        }
    }

    public class Ability
    {
        public string Name { get; set; }

        public string Text { get; set; }

        public string Type { get; set; }
    }

    public class Attack
    {
        public string Name { get; set; }

        public List<string> Cost { get; set; }

        public int ConvertedEnergyCost { get; set; }

        public string Damage { get; set; }

        public string Text { get; set; }
    }

    public class Weakness
    {
        public string Type { get; set; }

        public string Value { get; set; }
    }

    public class Resistance
    {
        public string Type { get; set; }

        public string Value { get; set; }
    }

    public class TCGPlayer
    {
        public string Url { get; set; }

        public DateTime LastUpdated { get; set; }

        public Dictionary<string, Price> CardPrices { get; set; }
    }

    public class Price
    {
        public float? Low { get; set; }

        public float? Mid { get; set; }

        public float? High { get; set; }

        public float? Market { get; set; }

        public float? DirectLow { get; set; }
    }
}
