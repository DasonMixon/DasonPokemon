using System.Collections.Generic;
using DasonPokemon.Core.Models;
using MongoDB.Extensions.Repository.Models;

namespace DasonPokemon.Core.Entities
{
    public class User : MongoEntity
    {
        public User()
        {
            Collection = new List<CollectionCard>();
        }

        public string Email { get; set; }
        public string PTCGOAccountId { get; set; }
        public List<CollectionCard> Collection { get; set; }
    }
}
