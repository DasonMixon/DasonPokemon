using MongoDB.Extensions.Repository.Models;

namespace DasonPokemon.Core.Entities
{
    public class Rarity : MongoEntity
    {
        public string Name { get; set; }
    }
}
