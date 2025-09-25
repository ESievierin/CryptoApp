using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoApp.Domain.Models;
using CryptoApp.Domain.Services;
using System.Collections.ObjectModel;

namespace CryptoApp.ApplicationCore.ViewModels
{
    public partial class ConverterViewModel(
        IMarketDataProvider marketDataProvider,
        INavigationManager navigationManager
    ) : ObservableObject
    {
        [ObservableProperty] 
        private decimal fromAmount = 1m;
        [ObservableProperty] 
        private decimal toAmount;

        [ObservableProperty] 
        private SearchCoin? fromCoin;
        [ObservableProperty] 
        private SearchCoin? toCoin;

        [ObservableProperty] 
        private string searchQuery = string.Empty;
        [ObservableProperty] 
        private string searchTitle = "Select coin";

        [ObservableProperty] 
        private ObservableCollection<SearchCoin> searchResults = new();
        [ObservableProperty] 
        private bool isSearchVisible;
        [ObservableProperty] 
        private bool isSelectingFrom;

        private decimal currentRate = 0m;

        public async Task InitializeAsync()
        {
            FromCoin = new SearchCoin
            {
                Id = "bitcoin",
                Name = "Bitcoin",
                Symbol = "BTC",
                Image = "https://assets.coingecko.com/coins/images/1/large/bitcoin.png"
            };
            ToCoin = new SearchCoin
            {
                Id = "ethereum",
                Name = "Ethereum",
                Symbol = "ETH",
                Image = "https://assets.coingecko.com/coins/images/279/large/ethereum.png"
            };

            await UpdateConversionAsync();
        }

        [RelayCommand]
        private void ShowFromSearch()
        {
            IsSelectingFrom = true;
            SearchTitle = "Select source coin";
            OpenSearch();
        }

        [RelayCommand]
        private void ShowToSearch()
        {
            IsSelectingFrom = false;
            SearchTitle = "Select target coin";
            OpenSearch();
        }

        private void OpenSearch()
        {
            IsSearchVisible = true;
            SearchQuery = string.Empty;
            SearchResults.Clear();
        }

        [RelayCommand]
        private async Task SearchCoinsAsync()
        {
            var query = SearchQuery?.Trim();
            if (string.IsNullOrWhiteSpace(query))
            {
                SearchResults.Clear();
                return;
            }

            var found = await marketDataProvider.SearchCoinsAsync(query);
            SearchResults.Clear();
            foreach (var c in found)
                SearchResults.Add(c);
        }

        [RelayCommand]
        private async Task SelectCoinAsync(SearchCoin coin)
        {
            if (IsSelectingFrom)
                FromCoin = coin;
            else
                ToCoin = coin;

            IsSearchVisible = false;
            await UpdateConversionAsync();
        }

        [RelayCommand]
        private async Task UpdateConversionAsync()
        {
            if (FromCoin is null || ToCoin is null)
                return;

            var rate = await marketDataProvider.GetConversionRateAsync(FromCoin.Id, ToCoin.Id);
            currentRate = rate;
            ToAmount = Round(FromAmount * rate);
        }

        partial void OnFromAmountChanged(decimal value)
        {
            if (currentRate > 0)
                ToAmount = Round(value * currentRate);
        }

        private static decimal Round(decimal value) => Math.Round(value, 11);

        [RelayCommand]
        private void GoBack() => navigationManager.NavigateTo<MainPageViewModel>();
    }
}
