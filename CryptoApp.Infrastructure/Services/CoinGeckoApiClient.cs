using CryptoApp.Domain.Models;
using CryptoApp.Domain.Services;
using CryptoApp.Infrastructure.DTO;
using CryptoApp.Infrastructure.Extensions.Maping;
using CryptoApp.Infrastructure.Extensions.Mapping;
using System.Text.Json;

namespace CryptoApp.Infrastructure.Services
{
    public sealed class CoinGeckoApiClient(HttpClient httpClient) : IMarketDataProvider
    {
        public async Task<List<CryptoCoin>> GetTopCoinsAsync(int count, string currency = "usd")
        {
            var url = $"coins/markets?vs_currency=usd&order=market_cap_desc&per_page={count}&page=1&sparkline=false&price_change_percentage=24h";

            using var resp = await httpClient.GetAsync(url);
            resp.EnsureSuccessStatusCode();

            var json = await resp.Content.ReadAsStringAsync();

            var coins = JsonSerializer.Deserialize<List<CoinGeckoCoinDto>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return coins.ToDomain();
        }
        public async Task<CoinDetail> GetCoinDetailAsync(string id, string currency = "usd")
        {
            var url = $"coins/{id}?localization=false&tickers=true&market_data=true&community_data=false&developer_data=false&sparkline=false";

            using var resp = await httpClient.GetAsync(url);
            resp.EnsureSuccessStatusCode();

            var json = await resp.Content.ReadAsStringAsync();
            var coinDetail = JsonSerializer.Deserialize<CoinGeckoCoinDetailDto>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (coinDetail is null)
                throw new InvalidOperationException($"Failed to parse coin detail for {id}");

           
            return coinDetail.ToDomain(currency);
        }
    }
}
