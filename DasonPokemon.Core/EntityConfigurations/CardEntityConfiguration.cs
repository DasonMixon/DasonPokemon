using DasonPokemon.Core.Entities;
using MongoDB.Driver;
using MongoDB.Extensions.Repository.Configuration;
using MongoDB.Extensions.Repository.Extensions;
using MongoDB.Extensions.Repository.Interfaces;

namespace DasonPokemon.Core.EntityConfigurations
{
    public class CardEntityConfiguration : IMongoEntityConfiguration<Card>
    {
        public void Configure(MongoEntityBuilder<Card> context)
        {
            context.Indexes.Add("external_id",
                Builders<Card>.IndexKeys.Ascending(x => x.ExternalId),
                o => o.WithCaseInsensitiveCollation());

            context.Indexes.Add("set_id",
                Builders<Card>.IndexKeys.Ascending(x => x.Set.Id),
                o => o.WithCaseInsensitiveCollation());

            context.Indexes.Add("name",
                Builders<Card>.IndexKeys.Text(x => x.Name));
        }
    }
}
