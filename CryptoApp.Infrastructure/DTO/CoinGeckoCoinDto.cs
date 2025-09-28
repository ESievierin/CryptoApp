using System.Text.Json.Serialization;

namespace CryptoApp.Infrastructure.DTO
{
    public sealed class CoinGeckoCoinDto
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("image")]
        public string? Image { get; set; }

        [JsonPropertyName("current_price")]
        public decimal? CurrentPrice { get; set; }

        [JsonPropertyName("market_cap")]
        public decimal? MarketCap { get; set; }

        [JsonPropertyName("fully_diluted_valuation")]
        public decimal? FullyDilutedValuation { get; set; }

        [JsonPropertyName("price_change_percentage_24h")]
        public decimal? PriceChangePercentage24h { get; set; }
    }
}
