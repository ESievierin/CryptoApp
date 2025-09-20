using AutoMapper;
using CryptoApp.Domain.Models;
using CryptoApp.Domain.Services;
using CryptoApp.Infrastructure.DTO;
using System.Text.Json;

namespace CryptoApp.Infrastructure.Services
{
    public sealed class CoinGeckoApiClient(HttpClient httpClient, IMapper mapper) : IMarketDataProvider
    {
        public async Task<List<CryptoCoin>> GetTopCoinsAsync(int count, string currency = "usd")
        {
            var url = $"coins/markets?vs_currency={currency}&order=market_cap_desc&per_page={count}&page=1&sparkline=false&price_change_percentage=24h";

            using var resp = await httpClient.GetAsync(url);
            resp.EnsureSuccessStatusCode();

            var json = await resp.Content.ReadAsStringAsync();

            var coins = JsonSerializer.Deserialize<List<CoinGeckoCoinDto>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return mapper.Map<List<CryptoCoin>>(coins) ?? new List<CryptoCoin>();
        }
    }
}
