using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ITM.Misc.EntitySECSeedDataGenerator
{
    internal class SECCompanyTicker
    {
        [JsonPropertyName("cik_str")]
        public uint CIK { get; set; }

        [JsonPropertyName("ticker")]
        public string TradingSymbol { get; set; }

        [JsonPropertyName("title")]
        public string Name { get; set; }

        public override string ToString()
        {
            return CIK.ToString() + " " + TradingSymbol + " " + Name;
        }


    }
}
