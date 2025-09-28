using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoApp.Infrastructure.DTO
{
    public sealed class MarketDataDto
    {
        [JsonPropertyName("current_price")]
        public Dictionary<string, decimal>? CurrentPrice { get; set; }

        [JsonPropertyName("market_cap")]
        public Dictionary<string, decimal>? MarketCap { get; set; }

        [JsonPropertyName("fully_diluted_valuation")]
        public Dictionary<string, decimal>? FullyDilutedValuation { get; set; }

        [JsonPropertyName("total_volume")]
        public Dictionary<string, decimal>? TotalVolume { get; set; }

        [JsonPropertyName("price_change_percentage_24h")]
        public decimal? PriceChangePercentage24h { get; set; }

        [JsonPropertyName("low_24h")]
        public Dictionary<string, decimal?>? Low24h { get; set; }

        [JsonPropertyName("high_24h")]
        public Dictionary<string, decimal?>? High24h { get; set; }
    }

}
