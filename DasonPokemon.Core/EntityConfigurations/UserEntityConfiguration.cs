using DasonPokemon.Core.Entities;
using MongoDB.Driver;
using MongoDB.Extensions.Repository.Configuration;
using MongoDB.Extensions.Repository.Extensions;
using MongoDB.Extensions.Repository.Interfaces;

namespace DasonPokemon.Core.EntityConfigurations
{
    public class UserEntityConfiguration : IMongoEntityConfiguration<User>
    {
        public void Configure(MongoEntityBuilder<User> context)
        {
            context.Indexes.Add("email",
                Builders<User>.IndexKeys.Ascending(x => x.Email),
                o => o.Unique());
        }
    }
}
