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
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<CryptoCoin[]> GetTopCoinsAsync(int count, string currency = "usd")
        {
            var url = $"coins/markets?vs_currency={currency}&order=market_cap_desc&per_page={count}&page=1&sparkline=false&price_change_percentage=24h";

            using var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var coins = JsonSerializer.Deserialize<CoinGeckoCoinDto[]>(json, JsonOptions)
                ?? Array.Empty<CoinGeckoCoinDto>();

            return coins.ToDomain();
        }

        public async Task<CoinDetail> GetCoinDetailAsync(string id, string currency = "usd")
        {
            var url = $"coins/{id}?localization=false&tickers=true&market_data=true&community_data=false&developer_data=false&sparkline=false";

            using var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var coinDetail = JsonSerializer.Deserialize<CoinGeckoCoinDetailDto>(json, JsonOptions);

            return coinDetail?.ToDomain(currency)
                   ?? new CoinDetail { Id = id, Symbol = "N/A", Name = "N/A" };
        }

        public async Task<SearchCoin[]> SearchCoinsAsync(string query)
        {
            var url = $"search?query={query}";
            using var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var searchResult = JsonSerializer.Deserialize<CoinGeckoSearchDto>(json, JsonOptions);

            return searchResult?.Coins.ToDomain() ?? Array.Empty<SearchCoin>();
        }

        public async Task<PricePoint[]> GetPriceSeriesAsync(string id, string currency = "usd", int days = 7)
        {
            var url = $"coins/{id}/market_chart?vs_currency={currency}&days={days}";
            using var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(json);

            if (!document.RootElement.TryGetProperty("prices", out var prices))
                return Array.Empty<PricePoint>();

            var points = prices.EnumerateArray()
                .Select(p => new PricePoint
                {
                    TimeOleAutomationDate = DateTimeOffset
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

            using var response = await httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return 0m;

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, decimal>>>(json, JsonOptions)
                ?? new Dictionary<string, Dictionary<string, decimal>>();

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
