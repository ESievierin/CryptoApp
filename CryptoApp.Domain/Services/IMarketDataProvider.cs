using CryptoApp.Domain.Models;

namespace CryptoApp.Domain.Services
{
    public interface IMarketDataProvider
    {
        Task<CryptoCoin[]> GetTopCoinsAsync(int count, string currency = "usd");
        Task<CoinDetail> GetCoinDetailAsync(string id, string currency = "usd");
        Task<SearchCoin[]> SearchCoinsAsync(string query);
        Task<PricePoint[]> GetPriceSeriesAsync(string id, string currency = "usd", int days = 7);
        Task<decimal> GetConversionRateAsync(string fromId, string toId, string vsCurrency = "usd");
    }
}
