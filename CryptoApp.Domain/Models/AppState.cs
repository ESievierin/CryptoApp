namespace CryptoApp.Domain.Models
{
    public sealed class AppState
    {
        public Currency SelectedCurrency { get; set; } = CurrencyCatalog.Usd;
        public Language SelectedLanguage { get; set; } = LanguageCatalog.English;
    }
}
