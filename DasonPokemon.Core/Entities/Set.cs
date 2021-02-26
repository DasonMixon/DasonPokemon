using System;
using System.Collections.Generic;
using MongoDB.Extensions.Repository.Models;

namespace DasonPokemon.Core.Entities
{
    public class Set : MongoEntity
    {
        public string PTCGOCode { get; set; }

        public string Name { get; set; }

        public string Series { get; set; }

        public int PrintedTotal { get; set; }

        public int Total { get; set; }

        public Dictionary<string, string> Legalities { get; set; } // Unlimited, Expanded, etc..

        public Dictionary<string, string> Images { get; set; } // Symbol, Logo, etc.. (Our custom pack image should be included in here)

        public DateTime ReleaseDate { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
