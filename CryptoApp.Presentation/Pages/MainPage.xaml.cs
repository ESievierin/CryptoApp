using CryptoApp.ApplicationCore.ViewModels;
using CryptoApp.Domain.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CryptoApp.Presentation.Pages
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && DataContext is MainPageViewModel vm &&
                vm.SearchCoinsCommand.CanExecute(null))
            {
                vm.SearchCoinsCommand.Execute(null);
            }
        }

        private void SearchResult_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element &&
                element.DataContext is SearchCoin coin &&
                DataContext is MainPageViewModel vm &&
                vm.OpenCoinCommand.CanExecute(coin.Id))
            {
                vm.OpenCoinCommand.Execute(coin.Id);

            }
        }
    }
}
