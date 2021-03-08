using System.Runtime.Serialization;

namespace DasonPokemon.Core
{
    public class Enums
    {
        public enum CardRarity
        {
            [EnumMember(Value = "Unknown")]
            Unknown = 0,
            [EnumMember(Value = "Common")]
            Common = 1,
            [EnumMember(Value = "Uncommon")]
            Uncommon = 2,
            [EnumMember(Value = "Amazing Rare")]
            AmazingRare = 3,
            [EnumMember(Value = "LEGEND")]
            Legend = 4,
            [EnumMember(Value = "Promo")]
            Promo = 5,
            [EnumMember(Value = "Rare")]
            Rare = 6,
            [EnumMember(Value = "Rare ACE")]
            RareAce = 7,
            [EnumMember(Value = "Rare BREAK")]
            RareBreak = 8,
            [EnumMember(Value = "Rare Holo")]
            RareHolo = 9,
            [EnumMember(Value = "Rare Holo EX")]
            RareHoloEx = 10,
            [EnumMember(Value = "Rare Holo GX")]
            RareHoloGx = 11,
            [EnumMember(Value = "Rare Holo LV.X")]
            RareHoloLvX = 12,
            [EnumMember(Value = "Rare Holo Star")]
            RareHoloStar = 13,
            [EnumMember(Value = "Rare Holo V")]
            RareHoloV = 14,
            [EnumMember(Value = "Rare Holo VMAX")]
            RareHoloVMax = 15,
            [EnumMember(Value = "Rare Prime")]
            RarePrime = 16,
            [EnumMember(Value = "Rare Prism Star")]
            RatePrismStar = 17,
            [EnumMember(Value = "Rare Rainbow")]
            RareRainbow = 18,
            [EnumMember(Value = "Rare Secret")]
            RareSecret = 19,
            [EnumMember(Value = "Rare Shining")]
            RareShining = 20,
            [EnumMember(Value = "Rare Shiny")]
            RareShiny = 21,
            [EnumMember(Value = "Rare Shiny GX")]
            RareShinyGx = 22,
            [EnumMember(Value = "Rare Ultra")]
            RareUltra = 23
        }
    }
}
