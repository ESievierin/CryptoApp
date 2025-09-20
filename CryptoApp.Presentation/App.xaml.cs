using CryptoApp.ApplicationCore.ViewModels;
using CryptoApp.Presentation.Extensions;
using CryptoApp.Presentation.Pages;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace CryptoApp.Presentation
{
    public partial class App : Application
    {
        private ServiceProvider services;

        public App()
        {
            var sc = new ServiceCollection();

            sc.AddConfiguration()
              .AddMapper()
              .AddApiClients()
              .AddViewModels()
              .AddViews();

            services = sc.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = new MainWindow();

            var mainPage = services.GetRequiredService<MainPage>();
            mainPage.DataContext = services.GetRequiredService<MainPageViewModel>();

            mainWindow.MainFrame.Navigate(mainPage);

            MainWindow = mainWindow;
            MainWindow.Show();
        }
    }
}
