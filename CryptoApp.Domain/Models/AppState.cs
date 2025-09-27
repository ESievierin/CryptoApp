namespace CryptoApp.Domain.Models
{
    public sealed class AppState
    {
        public Theme SelectedTheme { get; set; } = Theme.Dark;
        public Currency SelectedCurrency { get; set; } = CurrencyCatalog.Usd;
        public Language SelectedLanguage { get; set; } = LanguageCatalog.English;
    }
}
