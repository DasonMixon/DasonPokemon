using System;
using AutoMapper;

namespace DasonPokemon.Api
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Core.ApiResponses.Card, Core.Entities.Card>()
                .ForMember(d => d.ExternalId, s => s.MapFrom(src => src.Id))
                .ForMember(d => d.Abilities, s => s.MapFrom(src => src.Abilities))
                .ForMember(d => d.Artist, s => s.MapFrom(src => src.Artist))
                .ForMember(d => d.Attacks, s => s.MapFrom(src => src.Attacks))
                .ForMember(d => d.ConvertedRetreatCost, s => s.MapFrom(src => src.ConvertedRetreatCost))
                .ForMember(d => d.EvolvesFrom, s => s.MapFrom(src => src.EvolvesFrom))
                .ForMember(d => d.FlavorText, s => s.MapFrom(src => src.FlavorText))
                .ForMember(d => d.Hitpoints, s => s.MapFrom(src => src.Hitpoints))
                .ForMember(d => d.Images, s => s.MapFrom(src => src.Images))
                .ForMember(d => d.Legalities, s => s.MapFrom(src => src.Legalities))
                .ForMember(d => d.Name, s => s.MapFrom(src => src.Name))
                .ForMember(d => d.NationalPokedexNumbers, s => s.MapFrom(src => src.NationalPokedexNumbers))
                .ForMember(d => d.Number, s => s.MapFrom(src => src.Number))
                .ForMember(d => d.Rarity, s => s.MapFrom(src => src.Rarity))
                .ForMember(d => d.Resistances, s => s.MapFrom(src => src.Resistances))
                .ForMember(d => d.RetreatCost, s => s.MapFrom(src => src.RetreatCost))
                .ForMember(d => d.Set, s => s.MapFrom(src => src.Set))
                .ForMember(d => d.Subtypes, s => s.MapFrom(src => src.Subtypes))
                .ForMember(d => d.Supertype, s => s.MapFrom(src => src.Supertype))
                .ForMember(d => d.TCGPlayer, s => s.MapFrom(src => src.TCGPlayer))
                .ForMember(d => d.Types, s => s.MapFrom(src => src.Types))
                .ForMember(d => d.Weaknesses, s => s.MapFrom(src => src.Weaknesses))
                .ForMember(d => d.Id, s => s.MapFrom(src => Guid.NewGuid()));

            CreateMap<Core.ApiResponses.Ability, Core.Entities.Ability>()
                .ForMember(d => d.Name, s => s.MapFrom(src => src.Name))
                .ForMember(d => d.Text, s => s.MapFrom(src => src.Text))
                .ForMember(d => d.Type, s => s.MapFrom(src => src.Type));

            CreateMap<Core.ApiResponses.Attack, Core.Entities.Attack>()
                .ForMember(d => d.Name, s => s.MapFrom(src => src.Name))
                .ForMember(d => d.Text, s => s.MapFrom(src => src.Text))
                .ForMember(d => d.ConvertedEnergyCost, s => s.MapFrom(src => src.ConvertedEnergyCost))
                .ForMember(d => d.Cost, s => s.MapFrom(src => src.Cost))
                .ForMember(d => d.Damage, s => s.MapFrom(src => src.Damage));

            CreateMap<Core.ApiResponses.Resistance, Core.Entities.Resistance>()
                .ForMember(d => d.Type, s => s.MapFrom(src => src.Type))
                .ForMember(d => d.Value, s => s.MapFrom(src => src.Value));

            CreateMap<Core.ApiResponses.Weakness, Core.Entities.Weakness>()
                .ForMember(d => d.Type, s => s.MapFrom(src => src.Type))
                .ForMember(d => d.Value, s => s.MapFrom(src => src.Value));

            CreateMap<Core.ApiResponses.TCGPlayer, Core.Entities.TCGPlayer>()
                .ForMember(d => d.CardPrices, s => s.MapFrom(src => src.Prices))
                .ForMember(d => d.LastUpdated, s => s.MapFrom(src => DateTime.SpecifyKind(DateTime.Parse(src.UpdatedAt), DateTimeKind.Utc)))
                .ForMember(d => d.Url, s => s.MapFrom(src => src.Url));

            CreateMap<Core.ApiResponses.Price, Core.Entities.Price>()
                .ForMember(d => d.DirectLow, s => s.MapFrom(src => src.DirectLow))
                .ForMember(d => d.High, s => s.MapFrom(src => src.High))
                .ForMember(d => d.Low, s => s.MapFrom(src => src.Low))
                .ForMember(d => d.Market, s => s.MapFrom(src => src.Market))
                .ForMember(d => d.Mid, s => s.MapFrom(src => src.Mid));

            CreateMap<Core.ApiResponses.Set, Core.Entities.Set>()
                .ForMember(d => d.ExternalId, s => s.MapFrom(src => src.Id))
                .ForMember(d => d.Images, s => s.MapFrom(src => src.Images))
                .ForMember(d => d.LastUpdated, s => s.MapFrom(src => DateTime.SpecifyKind(DateTime.Parse(src.UpdatedAt), DateTimeKind.Utc)))
                .ForMember(d => d.Legalities, s => s.MapFrom(src => src.Legalities))
                .ForMember(d => d.Name, s => s.MapFrom(src => src.Name))
                .ForMember(d => d.PrintedTotal, s => s.MapFrom(src => src.PrintedTotal))
                .ForMember(d => d.PTCGOCode, s => s.MapFrom(src => src.PTCGOCode))
                .ForMember(d => d.ReleaseDate, s => s.MapFrom(src => DateTime.SpecifyKind(DateTime.Parse(src.ReleaseDate), DateTimeKind.Utc)))
                .ForMember(d => d.Series, s => s.MapFrom(src => src.Series))
                .ForMember(d => d.Total, s => s.MapFrom(src => src.Total))
                .ForMember(d => d.Id, s => s.MapFrom(src => Guid.NewGuid()));
        }
    }
}
