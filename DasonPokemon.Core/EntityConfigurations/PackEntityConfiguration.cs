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
                RarityRates = new Dictionary<Enums.CardRarity, double>
                {
                    { Enums.CardRarity.Common, 1 }, // Each common has same chance to be pulled across the board
                    { Enums.CardRarity.Uncommon, 1 }, // Each uncommon has same chance to be pulled across the board
                    { Enums.CardRarity.Rare, 60.12 },
                    { Enums.CardRarity.RareHolo, 16.76 },
                    { Enums.CardRarity.RareHoloV, 12.41 },
                    { Enums.CardRarity.RareShiny, 10 },
                    { Enums.CardRarity.RareHoloVMax, 4.17 },
                    { Enums.CardRarity.RareUltra, 2.88 },
                    { Enums.CardRarity.AmazingRare, 5.17 },
                    { Enums.CardRarity.RareRainbow, 1.42 },
                    { Enums.CardRarity.RareSecret, 0.51 }
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
                RarityRates = new Dictionary<Enums.CardRarity, double>
                {
                    { Enums.CardRarity.Common, 1 }, // Each common has same chance to be pulled across the board
                    { Enums.CardRarity.Uncommon, 1 }, // Each uncommon has same chance to be pulled across the board
                    { Enums.CardRarity.Rare, 60.12 },
                    { Enums.CardRarity.RareHolo, 16.76 },
                    { Enums.CardRarity.RareHoloV, 12.41 },
                    { Enums.CardRarity.RareHoloVMax, 4.17 },
                    { Enums.CardRarity.RareUltra, 2.88 },
                    { Enums.CardRarity.AmazingRare, 5.17 },
                    { Enums.CardRarity.RareRainbow, 1.42 },
                    { Enums.CardRarity.RareSecret, 0.51 }
                }
            });
        }
    }
}
