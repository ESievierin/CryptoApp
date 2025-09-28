 using System.Text.Json.Serialization;

    namespace CryptoApp.Infrastructure.DTO
    {
        public sealed class CoinGeckoCoinDetailDto
        {
            [JsonPropertyName("id")]
            public string? Id { get; set; }

            [JsonPropertyName("symbol")]
            public string? Symbol { get; set; }

            [JsonPropertyName("name")]
            public string? Name { get; set; }

            [JsonPropertyName("image")]
            public ImageInfoDto? Image { get; set; }

            [JsonPropertyName("market_data")]
            public MarketDataDto? MarketData { get; set; }

            [JsonPropertyName("tickers")]
            public TickerDto[]? Tickers { get; set; }
        }
    }