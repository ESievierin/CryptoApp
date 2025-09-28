using System.Text.Json.Serialization;

namespace CryptoApp.Infrastructure.DTO
{
    public sealed class TickerDto
    {
        [JsonPropertyName("market")]
        public MarketInfoDto? Market { get; set; }

        [JsonPropertyName("base")]
        public string? Base { get; set; }

        [JsonPropertyName("target")]
        public string? Target { get; set; }

        [JsonPropertyName("last")]
        public decimal? Last { get; set; }

        [JsonPropertyName("trade_url")]
        public string? TradeUrl { get; set; }
    }
}
