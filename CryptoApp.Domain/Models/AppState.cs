namespace CryptoApp.Domain.Models
{
    public sealed class AppState
    {
        public Currency SelectedCurrency { get; set; } = CurrencyCatalog.Usd;
    }
}
