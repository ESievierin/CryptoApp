namespace CryptoApp.Infrastructure.Extensions.Dictionary
{
    public static class GetCurrencyValueExtension
    {
        public static decimal GetCurrencyValue(this Dictionary<string, decimal>? dict, string currency)
        {
            if (dict == null)
                return 0;

            return dict.TryGetValue(currency, out var value) ? value : 0;
        }
        public static decimal GetCurrencyValue(this Dictionary<string, decimal?>? dict, string currency)
        {
            if (dict == null)
                return 0;

            return (dict.TryGetValue(currency, out var value) ? value : 0) ?? 0m;
        }
    }
}
