using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DasonPokemon.Core.ApiResponses
{
    public class Set
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("ptcgoCode")]
        public string PTCGOCode { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("series")]
        public string Series { get; set; }

        [JsonPropertyName("printedTotal")]
        public int PrintedTotal { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("legalities")]
        public Dictionary<string, string> Legalities { get; set; }

        [JsonPropertyName("images")]
        public Dictionary<string, string> Images { get; set; }

        [JsonPropertyName("releaseDate")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("updatedAt")]
        public string UpdatedAt { get; set; }
    }
}
