using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoApp.ApplicationCore.Services.Navigation;
using CryptoApp.Domain.Models;
using CryptoApp.Domain.Services;
using System.Diagnostics;

namespace CryptoApp.ApplicationCore.ViewModels
{
    public partial class CoinDetailViewModel(
        IMarketDataProvider marketDataProvider,
        INavigationManager navigationManager,
        AppState appState
    ) : ObservableObject, INavigationAware
    {
        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private CoinDetail? coin;

        [ObservableProperty]
        private PricePoint[] priceSeries = Array.Empty<PricePoint>();

        [ObservableProperty]
        private int selectedDays = 7;

        public Currency SelectedCurrency => appState.SelectedCurrency;

        public async Task OnNavigatedTo(object parameter)
        {
            if (parameter is not string coinId)
                return;

            await LoadCoinDetailAsync(coinId);
            await LoadPriceSeriesAsync(coinId);
        }

        [RelayCommand]
        private async Task LoadCoinDetailAsync(string id)
        {
            if (IsLoading) 
                return;

            try
            {
                IsLoading = true;
                var coinDetail = await marketDataProvider.GetCoinDetailAsync(id, appState.SelectedCurrency.Code);

                coinDetail.Markets = coinDetail.Markets
                    .Where(m => m.Pair.StartsWith($"{coinDetail.Symbol}/", StringComparison.OrdinalIgnoreCase))
                    .ToArray();

                Coin = coinDetail;
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task LoadPriceSeriesAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return;

            try
            {
                IsLoading = true;
                PriceSeries = await marketDataProvider.GetPriceSeriesAsync(id, appState.SelectedCurrency.Code, SelectedDays);
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private void OpenMarket(string? url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return;

            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        [RelayCommand]
        private async Task ChangeDaysAsync(string stringDays)
        {
            if (!int.TryParse(stringDays, out var days) || Coin is null) 
                return;

            SelectedDays = days;
            await LoadPriceSeriesAsync(Coin.Id);
        }

        [RelayCommand]
        private void GoBack()
        {
            navigationManager.NavigateTo<MainPageViewModel>();
        }
    }
}
