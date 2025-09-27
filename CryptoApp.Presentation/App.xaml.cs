using CryptoApp.ApplicationCore.ViewModels;
using CryptoApp.Domain.Services;
using CryptoApp.Presentation.Extensions;
using CryptoApp.Presentation.Localization;
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
            var mainWindow = new MainWindow();
            sc.AddConfiguration()
               .AddResources()
               .AddTheme()
               .AddLocalization()
               .AddAppState()
               .AddApiClients()
               .AddViewModels()
               .AddViews()
               .AddSingleton(mainWindow)
               .AddNavigation(mainWindow.MainFrame); 

            services = sc.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var localization = services.GetRequiredService<ILocalizationService>();
            LocalizationProvider.Initialize(localization);

            var mainWindow = services.GetRequiredService<MainWindow>();
            MainWindow = mainWindow;

            var navigation = services.GetRequiredService<INavigationManager>();
            navigation.NavigateTo<MainPageViewModel>();

            MainWindow.Show();
        }

    }
}
