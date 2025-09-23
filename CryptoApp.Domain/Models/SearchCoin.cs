namespace CryptoApp.Domain.Models
{
    public sealed class SearchCoin
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Symbol { get; set; } = null!;
        public string Image { get; set; } = null!;
    }
}
