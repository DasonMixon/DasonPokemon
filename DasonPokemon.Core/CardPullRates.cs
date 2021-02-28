using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DasonPokemon.Core
{
    public static class CardPullRates
    {
        // https://cardzard.com/blogs/news/vivid-voltage-pull-rate-data
        public static Dictionary<string, double> VividVoltage = new Dictionary<string, double>
        {
            { "Rare", 60.12 },
            { "Rare Holo", 16.76 },
            { "Rare Holo V", 12.41 },
            { "Rare Holo VMAX", 4.17 },
            { "Rare Ultra", 2.88 },
            { "Amazing Rare", 5.17 },
            { "Rare Rainbow", 1.42 },
        };
    }
}
