using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoApp.Domain.Models;
using CryptoApp.Domain.Services;

namespace CryptoApp.ApplicationCore.ViewModels
{
    public partial class MainPageViewModel(
        IMarketDataProvider marketDataProvider,
        INavigationManager navigationManager
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

        [RelayCommand]
        public async Task LoadCoinsAsync()
        {
            if (IsLoading) return;

            try
            {
                IsLoading = true;
                Coins = await marketDataProvider.GetTopCoinsAsync(CoinCount);
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private void OpenCoin(string coinId)
        {
            if (string.IsNullOrWhiteSpace(coinId))
                return;

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
