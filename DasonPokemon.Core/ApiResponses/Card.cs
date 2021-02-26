using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DasonPokemon.Core.ApiResponses
{
    public class Card
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("supertype")]
        public string Supertype { get; set; }

        [JsonPropertyName("subtypes")]
        public List<string> Subtypes { get; set; }

        [JsonPropertyName("hp")]
        public string Hitpoints { get; set; }

        [JsonPropertyName("types")]
        public List<string> Types { get; set; }

        [JsonPropertyName("evolvesFrom")]
        public string EvolvesFrom { get; set; }

        [JsonPropertyName("abilities")]
        public List<Ability> Abilities { get; set; }

        [JsonPropertyName("attacks")]
        public List<Attack> Attacks { get; set; }

        [JsonPropertyName("weaknesses")]
        public List<Weakness> Weaknesses { get; set; }

        [JsonPropertyName("resistances")]
        public List<Resistance> Resistances { get; set; }

        [JsonPropertyName("retreatCost")]
        public List<string> RetreatCost { get; set; }

        [JsonPropertyName("convertedRetreatCost")]
        public int ConvertedRetreatCost { get; set; }

        [JsonPropertyName("set")]
        public Set Set { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("artist")]
        public string Artist { get; set; }

        [JsonPropertyName("rarity")]
        public string Rarity { get; set; }

        [JsonPropertyName("flavorText")]
        public string FlavorText { get; set; }

        [JsonPropertyName("nationalPokedexNumbers")]
        public List<int> NationalPokedexNumbers { get; set; }

        [JsonPropertyName("legalities")]
        public Dictionary<string, string> Legalities { get; set; }

        [JsonPropertyName("images")]
        public Dictionary<string, string> Images { get; set; }

        [JsonPropertyName("tcgplayer")]
        public TCGPlayer TCGPlayer { get; set; }
    }

    public class Ability
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class Attack
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("cost")]
        public List<string> Cost { get; set; }

        [JsonPropertyName("convertedEnergyCost")]
        public int ConvertedEnergyCost { get; set; }

        [JsonPropertyName("damage")]
        public string Damage { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public class Weakness
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }

    public class Resistance
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }

    public class TCGPlayer
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonPropertyName("prices")]
        public Dictionary<string, Price> Prices { get; set; }
    }

    public class Price
    {
        [JsonPropertyName("low")]
        public float? Low { get; set; }

        [JsonPropertyName("mid")]
        public float? Mid { get; set; }

        [JsonPropertyName("high")]
        public float? High { get; set; }

        [JsonPropertyName("market")]
        public float? Market { get; set; }

        [JsonPropertyName("directLow")]
        public float? DirectLow { get; set; }
    }
}
