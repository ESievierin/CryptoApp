using CryptoApp.Domain.Models;
using CryptoApp.Infrastructure.DTO;

namespace CryptoApp.Infrastructure.Extensions.Maping
{
    public static class CoinGeckoCoinDtoMappingExtension
    {
        public static CryptoCoin ToDomain(this CoinGeckoCoinDto coin) 
        {
            return new CryptoCoin
            {
                Id = coin.Id,
                Symbol = coin.Symbol,
                Name = coin.Name,
                CurrentPrice = coin.CurrentPrice,
                MarketCap = coin.MarketCap,
                FullyDilutedValuation = coin.FullyDilutedValuation,
                PriceChangePercentage24h = coin.PriceChangePercentage24h,
                Image = coin.Image
            };
        }

        public static CryptoCoin[] ToDomain(this IEnumerable<CoinGeckoCoinDto>? coins)
        {
            return coins?.Select(c => c.ToDomain()).ToArray() ?? Array.Empty<CryptoCoin>();
        }
    }
}
