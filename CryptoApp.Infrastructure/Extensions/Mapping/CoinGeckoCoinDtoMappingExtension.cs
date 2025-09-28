using CryptoApp.Domain.Models;
using CryptoApp.Infrastructure.DTO;
using CryptoApp.Infrastructure.Extensions.String;

namespace CryptoApp.Infrastructure.Extensions.Maping
{
    public static class CoinGeckoCoinDtoMappingExtension
    {
        public static CryptoCoin ToDomain(this CoinGeckoCoinDto coin)
        {
            return new CryptoCoin
            {
                Id = coin.Id.OrNA(),
                Symbol = coin.Symbol.OrNA(),
                Name = coin.Name.OrNA(),
                CurrentPrice = coin.CurrentPrice ?? default,
                MarketCap = coin.MarketCap ?? default,
                FullyDilutedValuation = coin.FullyDilutedValuation ?? default,
                PriceChangePercentage24h = coin.PriceChangePercentage24h ?? default,
                Image = coin.Image ?? string.Empty
            };
        }

        public static CryptoCoin[] ToDomain(this IEnumerable<CoinGeckoCoinDto>? coins)
        {
            return coins?.Select(c => c.ToDomain()).ToArray() ?? Array.Empty<CryptoCoin>();
        }
    }
}
