using CryptoApp.Domain.Models;
using CryptoApp.Infrastructure.DTO;

namespace CryptoApp.Infrastructure.Extensions.Mapping
{
    public static class CoinGeckoSearchCoinDtoMappingExtension
    {
        public static SearchCoin ToDomain(this CoinGeckoSearchCoinDto coin)
        {
            return new SearchCoin
            {
                Id = coin.Id,
                Name = coin.Name,
                Symbol = coin.Symbol,
                Image = coin.Image
            };
        }

        public static SearchCoin[] ToDomain(this IEnumerable<CoinGeckoSearchCoinDto>? dtos)
        {
            return dtos?.Select(d => d.ToDomain()).ToArray() ?? Array.Empty<SearchCoin>();
        }
    }
}
