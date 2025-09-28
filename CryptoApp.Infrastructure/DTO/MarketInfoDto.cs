using System.Text.Json.Serialization;

namespace CryptoApp.Infrastructure.DTO
{
    public sealed class MarketInfoDto
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}
