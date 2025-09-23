using CryptoApp.Domain.Models;
using CryptoApp.Infrastructure.DTO;
using CryptoApp.Infrastructure.Extensions.Dictionary;

namespace CryptoApp.Infrastructure.Extensions.Mapping
{
    public static class CoinGeckoCoinDetailDtoMappingExtension
    {
        public static CoinDetail ToDomain(this CoinGeckoCoinDetailDto coinDetail, string currency)
        {
            if (coinDetail is null)
                throw new ArgumentNullException(nameof(coinDetail));

            return new CoinDetail
            {
                Id = coinDetail.Id,
                Symbol = coinDetail.Symbol,
                Name = coinDetail.Name,
                Image = coinDetail.Image.Large,
                CurrentPrice = coinDetail.MarketData.CurrentPrice.GetCurrencyValue(currency),
                MarketCap = coinDetail.MarketData.MarketCap.GetCurrencyValue(currency),
                FullyDilutedValuation = coinDetail.MarketData.FullyDilutedValuation.GetCurrencyValue(currency),
                TotalVolume = coinDetail.MarketData.TotalVolume.GetCurrencyValue(currency),
                PriceChangePercentage24h = coinDetail.MarketData.PriceChangePercentage24h,
                Min24h = coinDetail.MarketData.Low24h.GetCurrencyValue(currency),
                Max24h = coinDetail.MarketData.High24h.GetCurrencyValue(currency),
                Markets = coinDetail.Tickers.Select(t => new CoinMarket
                {
                    Exchange = t.Market.Name,
                    Pair = $"{t.Base}/{t.Target}",
                    Price = t.Last,
                    TradeUrl = t.TradeUrl
                }).ToArray()
            };
        }
    }
}
