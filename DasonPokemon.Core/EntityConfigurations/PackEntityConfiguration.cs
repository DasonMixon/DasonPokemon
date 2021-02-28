using MongoDB.Extensions.Repository.Configuration;
using MongoDB.Extensions.Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace DasonPokemon.Core.EntityConfigurations
{
    public class PackEntityConfiguration : IMongoEntityConfiguration<Pack>
    {
        private readonly Guid ShiningFatesPackId = Guid.Parse("a9447188-d65a-4135-83d6-c5060ab2a904");
        private readonly Guid VividVoltagePackId = Guid.Parse("9c6959c7-e8be-4717-85ed-18f5d95385c3");

        public void Configure(MongoEntityBuilder<Pack> context)
        {
            context.Seed.Add(new Pack
            {
                Id = ShiningFatesPackId,
                Name = "Shining Fates",
                SetIds = new List<Guid>(), // These will be populated by the data refresh
                ExternalSetIds = new List<string> { "swsh45", "swsh45sv" },
                ExternalId = null,
                RarityRates = new Dictionary<string, double>
                {
                    { "Common", 1 }, // Each common has same chance to be pulled across the board
                    { "Uncommon", 1 }, // Each uncommon has same chance to be pulled across the board
                    { "Rare", 60.12 },
                    { "Rare Holo", 16.76 },
                    { "Rare Holo V", 12.41 },
                    { "Rare Shiny", 10 },
                    { "Rare Holo VMAX", 4.17 },
                    { "Rare Ultra", 2.88 },
                    { "Amazing Rare", 5.17 },
                    { "Rare Rainbow", 1.42 },
                    { "Rare Secret", 0.51 }
                },
                Image = "/assets/shining_fates_pack.jpg"
            });

            context.Seed.Add(new Pack
            {
                Id = VividVoltagePackId,
                Name = "Vivid Voltage",
                SetIds = new List<Guid>(), // These will be populated by the data refresh
                ExternalSetIds = new List<string> { "swsh4" },
                ExternalId = null,
                Image = "/assets/vivid_voltage_pack.jpg",
                RarityRates = new Dictionary<string, double>
                {
                    { "Common", 1 }, // Each common has same chance to be pulled across the board
                    { "Uncommon", 1 }, // Each uncommon has same chance to be pulled across the board
                    { "Rare", 60.12 },
                    { "Rare Holo", 16.76 },
                    { "Rare Holo V", 12.41 },
                    { "Rare Holo VMAX", 4.17 },
                    { "Rare Ultra", 2.88 },
                    { "Amazing Rare", 5.17 },
                    { "Rare Rainbow", 1.42 },
                    { "Rare Secret", 0.51 }
                }
            });
        }
    }
}
