using MongoDB.Extensions.Repository.Models;

namespace DasonPokemon.Core.Entities
{
    public class Type : MongoEntity
    {
        public string Name { get; set; }
    }
}
