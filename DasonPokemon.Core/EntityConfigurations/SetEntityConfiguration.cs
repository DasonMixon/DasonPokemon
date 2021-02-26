using DasonPokemon.Core.Entities;
using MongoDB.Driver;
using MongoDB.Extensions.Repository.Configuration;
using MongoDB.Extensions.Repository.Extensions;
using MongoDB.Extensions.Repository.Interfaces;

namespace DasonPokemon.Core.EntityConfigurations
{
    public class SetEntityConfiguration : IMongoEntityConfiguration<Set>
    {
        public void Configure(MongoEntityBuilder<Set> context)
        {
            context.Indexes.Add("name",
                Builders<Set>.IndexKeys.Text(x => x.Name));
        }
    }
}
