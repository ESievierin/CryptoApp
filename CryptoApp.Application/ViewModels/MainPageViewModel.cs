using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoApp.Domain.Models;
using CryptoApp.Domain.Services;

namespace CryptoApp.ApplicationCore.ViewModels
{
    public partial class MainPageViewModel(IMarketDataProvider marketDataProvider) : ObservableObject
    {
        [ObservableProperty]
        private bool isLoading;

        private const int CoinCount = 10;

        [ObservableProperty]
        private List<CryptoCoin> coins = new();


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
    }
}
