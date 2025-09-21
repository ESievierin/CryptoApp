namespace CryptoApp.Domain.Models
{
    public sealed class CoinMarket
    {
        public string Exchange { get; set; } = null!;
        public string Pair { get; set; } = null!;
        public decimal Price { get; set; }
        public string TradeUrl { get; set; } = null!;
    }
}
