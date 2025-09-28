using System.Text.Json.Serialization;

namespace CryptoApp.Infrastructure.DTO
{
    public sealed class ImageInfoDto
    {
        [JsonPropertyName("large")]
        public string? Large { get; set; }
    }
}
