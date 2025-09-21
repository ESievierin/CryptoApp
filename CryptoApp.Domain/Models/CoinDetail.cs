namespace CryptoApp.Domain.Models
{
    public sealed class CoinDetail
    {
        public string Id { get; set; } = null!;
        public string Symbol { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public decimal CurrentPrice { get; set; }
        public decimal MarketCap { get; set; }
        public decimal FullyDilutedValuation { get; set; }
        public decimal TotalVolume { get; set; }
        public decimal PriceChangePercentage24h { get; set; }
        public decimal Min24h { get; set; }
        public decimal Max24h { get; set; }

        public IReadOnlyList<CoinMarket> Markets { get; set; } = [];
    }
}
