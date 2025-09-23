using System.Text.Json.Serialization;

namespace CryptoApp.Infrastructure.DTO
{
    public sealed class CoinGeckoCoinDetailDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = null!;

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("image")]
        public ImageInfoDto Image { get; set; } = null!;

        [JsonPropertyName("market_data")]
        public MarketDataDto MarketData { get; set; } = null!;

        [JsonPropertyName("tickers")]
        public TickerDto[] Tickers { get; set; } = Array.Empty<TickerDto>();
    }

    public sealed class ImageInfoDto
    {
        [JsonPropertyName("large")]
        public string Large { get; set; } = null!;
    }

    public sealed class MarketDataDto
    {
        [JsonPropertyName("current_price")]
        public Dictionary<string, decimal> CurrentPrice { get; set; } = null!;

        [JsonPropertyName("market_cap")]
        public Dictionary<string, decimal> MarketCap { get; set; } = null!;

        [JsonPropertyName("fully_diluted_valuation")]
        public Dictionary<string, decimal> FullyDilutedValuation { get; set; } = null!;

        [JsonPropertyName("total_volume")]
        public Dictionary<string, decimal> TotalVolume { get; set; } = null!;

        [JsonPropertyName("price_change_percentage_24h")]
        public decimal PriceChangePercentage24h { get; set; }

        [JsonPropertyName("low_24h")]
        public Dictionary<string, decimal> Low24h { get; set; } = null!;

        [JsonPropertyName("high_24h")]
        public Dictionary<string, decimal> High24h { get; set; } = null!;
    }


    public sealed class TickerDto
    {
        [JsonPropertyName("market")]
        public MarketInfoDto Market { get; set; } = null!;

        [JsonPropertyName("base")]
        public string Base { get; set; } = null!;

        [JsonPropertyName("target")]
        public string Target { get; set; } = null!;

        [JsonPropertyName("last")]
        public decimal Last { get; set; }

        [JsonPropertyName("trade_url")]
        public string TradeUrl { get; set; } = null!;
    }

    public sealed class MarketInfoDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
    }
}
