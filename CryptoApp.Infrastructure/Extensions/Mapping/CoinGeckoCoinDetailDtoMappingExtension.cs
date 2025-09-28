using CryptoApp.Domain.Models;
using CryptoApp.Infrastructure.DTO;
using CryptoApp.Infrastructure.Extensions.Dictionary;
using CryptoApp.Infrastructure.Extensions.String;

namespace CryptoApp.Infrastructure.Extensions.Mapping
{
    public static class CoinGeckoCoinDetailDtoMappingExtension
    {
        public static CoinDetail ToDomain(this CoinGeckoCoinDetailDto coinDetail, string currency)
        {
            return new CoinDetail
            {
                Id = coinDetail.Id.OrNA(),
                Symbol = coinDetail.Symbol.OrNA(),
                Name = coinDetail.Name.OrNA(),
                Image = coinDetail.Image?.Large ?? string.Empty,
                CurrentPrice = coinDetail?.MarketData?.CurrentPrice.GetCurrencyValue(currency) ?? default,
                MarketCap = coinDetail?.MarketData?.MarketCap.GetCurrencyValue(currency) ?? default,
                FullyDilutedValuation = coinDetail?.MarketData?.FullyDilutedValuation.GetCurrencyValue(currency) ?? default,
                TotalVolume = coinDetail?.MarketData?.TotalVolume.GetCurrencyValue(currency) ?? default,
                PriceChangePercentage24h = coinDetail?.MarketData?.PriceChangePercentage24h ?? default,
                Min24h = coinDetail?.MarketData?.Low24h.GetCurrencyValue(currency) ?? default,
                Max24h = coinDetail?.MarketData?.High24h.GetCurrencyValue(currency) ?? default,
                Markets = coinDetail?.Tickers?
                    .Select(t => new CoinMarket
                    {
                        Exchange = t.Market?.Name.OrNA() ?? "N/A",
                        Pair = $"{t.Base.OrNA()}/{t.Target.OrNA()}",
                        Price = t.Last ?? default,
                        TradeUrl = t.TradeUrl ?? string.Empty
                    })
                    .ToArray() ?? Array.Empty<CoinMarket>()
            }; 
        }
    }
}
