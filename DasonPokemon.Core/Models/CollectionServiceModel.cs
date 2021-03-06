using System.Collections.Generic;

namespace DasonPokemon.Core.Models
{
    public class CollectionServiceModel
    {
        public string AccountId { get; set; }
        public IEnumerable<CollectionItem> Collection { get; set; }
    }

    public class CollectionItem
    {
        public string PTCGOCode { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }
    }
}
