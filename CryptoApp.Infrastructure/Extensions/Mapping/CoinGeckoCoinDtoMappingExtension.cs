using CryptoApp.Domain.Models;
using CryptoApp.Infrastructure.DTO;
using System.Runtime.CompilerServices;

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

        public static List<CryptoCoin> ToDomain(this IEnumerable<CoinGeckoCoinDto>? coins)
        {
            return coins?.Select(c => c.ToDomain()).ToList() ?? new List<CryptoCoin>();
        }
    }
}
