using System.Text.Json.Serialization;

namespace CryptoApp.Infrastructure.DTO
{
    public sealed class CoinGeckoSearchDto
    {
        [JsonPropertyName("coins")]
        public CoinGeckoSearchCoinDto[] Coins { get; set; } = Array.Empty<CoinGeckoSearchCoinDto>();
    }

    public sealed class CoinGeckoSearchCoinDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = null!;
        [JsonPropertyName("large")]
        public string Image { get; set; } = null!;
    }
}
