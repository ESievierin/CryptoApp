using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoApp.Domain.Models;
using CryptoApp.Domain.Services;
using CryptoApp.Presentation.Services.Navigation;
using System.Diagnostics;

namespace CryptoApp.ApplicationCore.ViewModels
{
    public partial class CoinDetailViewModel(IMarketDataProvider marketDataProvider, INavigationManager navigationManager)
        : ObservableObject, INavigationAware
    {
        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private CoinDetail? coin;

        public async void OnNavigatedTo(object parameter)
        {
            if (parameter is not string coinId)
                return;

            await LoadCoinDetailAsync(coinId);
        }

        [RelayCommand]
        private async Task LoadCoinDetailAsync(string id)
        {
            if (IsLoading) return;

            try
            {
                IsLoading = true;
                var detail = await marketDataProvider.GetCoinDetailAsync(id);

                detail.Markets = detail.Markets
                .Where(m => m.Pair.StartsWith($"{detail.Symbol}/", StringComparison.OrdinalIgnoreCase))
                .ToList();

                Coin = detail;
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
        private void GoBack()
        {
            navigationManager.NavigateTo<MainPageViewModel>();
        }
    }
}
