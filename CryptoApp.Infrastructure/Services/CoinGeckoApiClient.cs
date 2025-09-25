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
        public async Task<CryptoCoin[]> GetTopCoinsAsync(int count, string currency = "usd")
        {
            var url = $"coins/markets?vs_currency={currency}&order=market_cap_desc&per_page={count}&page=1&sparkline=false&price_change_percentage=24h";

            using var resp = await httpClient.GetAsync(url);
            resp.EnsureSuccessStatusCode();

            var json = await resp.Content.ReadAsStringAsync();

            var coins = JsonSerializer.Deserialize<CoinGeckoCoinDto[]>(json,
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

        public async Task<SearchCoin[]> SearchCoinsAsync(string query)
        {
            var url = $"search?query={query}";
            using var resp = await httpClient.GetAsync(url);
            resp.EnsureSuccessStatusCode();

            var json = await resp.Content.ReadAsStringAsync();
            var searchResult = JsonSerializer.Deserialize<CoinGeckoSearchDto>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (searchResult is null)
                throw new InvalidOperationException($"Failed to search for {query}");

            return searchResult.Coins.ToDomain();
        }

        public async Task<PricePoint[]> GetPriceSeriesAsync(string id, string currency = "usd", int days = 7)
        {
            var url = $"coins/{id}/market_chart?vs_currency={currency}&days={days}";
            using var resp = await httpClient.GetAsync(url);
            resp.EnsureSuccessStatusCode();

            var json = await resp.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var prices = doc.RootElement.GetProperty("prices");
            
            var points = prices.EnumerateArray()
                .Select(p => new PricePoint
                {
                    TimeOADate = DateTimeOffset
                        .FromUnixTimeMilliseconds((long)p[0].GetDouble())
                        .UtcDateTime
                        .ToOADate(),
                    Price = p[1].GetDouble()
                })
                .ToArray();

            return points;
        }

        public async Task<decimal> GetConversionRateAsync(string fromId, string toId, string vsCurrency = "usd")
        {
            var ids = $"{fromId},{toId}";
            var url = $"simple/price?ids={ids}&vs_currencies={vsCurrency}";

            using var resp = await httpClient.GetAsync(url);
            if (!resp.IsSuccessStatusCode)
                return 0m;

            var json = await resp.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, decimal>>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (data is null)
                return 0m;

            decimal GetPrice(string coinId) =>
                data.TryGetValue(coinId.ToLowerInvariant(), out var map) &&
                map.TryGetValue(vsCurrency, out var price)
                    ? price
                    : 0m;

            var fromPrice = GetPrice(fromId);
            var toPrice = GetPrice(toId);

            return (fromPrice > 0 && toPrice > 0)
                ? fromPrice / toPrice
                : 0m;
        }

    }
}
