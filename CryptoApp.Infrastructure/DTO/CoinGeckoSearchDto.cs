using System.Text.Json.Serialization;

namespace CryptoApp.Infrastructure.DTO
{
    public sealed class CoinGeckoSearchDto
    {
        [JsonPropertyName("coins")]
        public CoinGeckoSearchCoinDto[] Coins { get; set; } = Array.Empty<CoinGeckoSearchCoinDto>();
    }
}
