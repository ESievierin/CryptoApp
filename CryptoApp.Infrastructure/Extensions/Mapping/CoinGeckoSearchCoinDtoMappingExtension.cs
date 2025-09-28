using CryptoApp.Domain.Models;
using CryptoApp.Infrastructure.DTO;
using CryptoApp.Infrastructure.Extensions.String;

namespace CryptoApp.Infrastructure.Extensions.Mapping
{
    public static class CoinGeckoSearchCoinDtoMappingExtension
    {
        public static SearchCoin ToDomain(this CoinGeckoSearchCoinDto coin)
        {
            return new SearchCoin
            {
                Id = coin.Id.OrNA(),
                Name = coin.Name.OrNA(),
                Symbol = coin.Symbol.OrNA(),
                Image = coin.Image ?? string.Empty
            };
        }

        public static SearchCoin[] ToDomain(this IEnumerable<CoinGeckoSearchCoinDto>? dtos)
        {
            return dtos?.Select(d => d.ToDomain()).ToArray() ?? Array.Empty<SearchCoin>();
        }
    }
}
