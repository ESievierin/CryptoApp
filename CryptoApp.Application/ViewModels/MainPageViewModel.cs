using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoApp.Domain.Models;
using CryptoApp.Domain.Services;

namespace CryptoApp.ApplicationCore.ViewModels
{
    public partial class MainPageViewModel(
        IMarketDataProvider marketDataProvider,
        INavigationManager navigationManager,
        ILocalizationService localizationService,
        AppState appState
    ) : ObservableObject
    {
        [ObservableProperty] 
        private bool isLoading;

        private const int CoinCount = 10;

        [ObservableProperty]
        private CryptoCoin[] coins = Array.Empty<CryptoCoin>();

        [ObservableProperty] 
        private string searchQuery = string.Empty;

        [ObservableProperty] 
        private SearchCoin[] searchResults = Array.Empty<SearchCoin>();

        [ObservableProperty] 
        private bool isSearchResultsVisible;

        public Currency SelectedCurrency
        {
            get => appState.SelectedCurrency;
            set
            {
                if (appState.SelectedCurrency != value)
                {
                    appState.SelectedCurrency = value;
                    OnPropertyChanged();
                    _ = LoadCoinsAsync();
                }
            }
        }

        public Language SelectedLanguage
        {
            get => appState.SelectedLanguage;
            set
            {
                if (appState.SelectedLanguage != value)
                {
                    appState.SelectedLanguage = value;
                    localizationService.SetLanguage(SelectedLanguage);
                    OnPropertyChanged();
                }
            }
        }
        public static IReadOnlyList<Currency> AvailableCurrencies => CurrencyCatalog.All;
        public static IReadOnlyList<Language> AvailableLanguages => LanguageCatalog.All;

        [RelayCommand]
        public async Task LoadCoinsAsync()
        {
            if (IsLoading) return;

            try
            {
                IsLoading = true;
                Coins = await marketDataProvider.GetTopCoinsAsync(
                    CoinCount,
                    appState.SelectedCurrency.Code
                );
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private void OpenCoin(string coinId)
        {
            if (!string.IsNullOrWhiteSpace(coinId))
                navigationManager.NavigateTo<CoinDetailViewModel>(coinId);
        }

        [RelayCommand]
        private async Task SearchCoinsAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                SearchResults = Array.Empty<SearchCoin>();
                IsSearchResultsVisible = false;
                return;
            }

            var results = await marketDataProvider.SearchCoinsAsync(SearchQuery);
            SearchResults = results;
            IsSearchResultsVisible = results.Length > 0;
        }

        [RelayCommand]
        private void OpenConverter()
        {
            navigationManager.NavigateTo<ConverterViewModel>();
        }
    }
}
