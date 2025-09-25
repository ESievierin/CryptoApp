namespace CryptoApp.Domain.Models
{
    public record Currency(string Code, string Symbol);

    public static class CurrencyCatalog
    {
        public static readonly Currency Usd = new("usd", "$");
        public static readonly Currency Eur = new("eur", "€");
        public static readonly Currency Gbp = new("gbp", "£");
        public static readonly Currency Uah = new("uah", "₴");

        public static readonly Currency[] All =
        {
            Usd, Eur, Gbp, Uah
        };
    }
}
