using CryptoApp.Domain.Models;

namespace CryptoApp.Domain.Services
{
    public interface IMarketDataProvider
    {
        Task<List<CryptoCoin>> GetTopCoinsAsync(int count, string currency = "usd");
    }
}
