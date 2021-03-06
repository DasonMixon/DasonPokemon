using MongoDB.Extensions.Repository.Models;

namespace DasonPokemon.Core.Entities
{
    public class User : MongoEntity
    {
        public string Email { get; set; }
        public string PTCGOAccountId { get; set; }
    }
}
