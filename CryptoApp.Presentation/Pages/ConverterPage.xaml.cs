using CryptoApp.ApplicationCore.ViewModels;
using CryptoApp.Domain.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CryptoApp.Presentation.Pages
{
    public partial class ConverterPage : Page
    {
        public ConverterPage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ConverterViewModel vm)
                await vm.InitializeAsync();
        }
    }
}
