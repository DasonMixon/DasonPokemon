using MongoDB.Extensions.Repository.Models;

namespace DasonPokemon.Core.Entities
{
    public class User : MongoEntity
    {
        public string Email { get; set; }
        public string PTCGOAccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Hash { get; set; }
    }
}
