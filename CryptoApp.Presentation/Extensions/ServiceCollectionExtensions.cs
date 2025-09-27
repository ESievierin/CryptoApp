using CryptoApp.ApplicationCore.ViewModels;
using CryptoApp.Domain.Models;
using CryptoApp.Domain.Services;
using CryptoApp.Infrastructure.Services;
using CryptoApp.Presentation.Pages;
using CryptoApp.Presentation.Services;
using CryptoApp.Presentation.Services.Navigation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Resources;
using System.Windows.Controls;

namespace CryptoApp.Presentation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings-local.json", optional: false, reloadOnChange: true)
                .Build();

            services.AddSingleton<IConfiguration>(config);

            return services;
        }
        
        public static IServiceCollection AddResources(this IServiceCollection services)
        {
           services.AddSingleton<ResourceManager>(rm =>
            new ResourceManager("CryptoApp.Presentation.Resources.Localization.Strings", typeof(ServiceCollectionExtensions).Assembly));

            return services;
        }
        
        public static IServiceCollection AddTheme(this IServiceCollection services) 
        {
            services.AddSingleton<IThemeService, ThemeService>();
            return services;
        }

        public static IServiceCollection AddLocalization(this IServiceCollection services)
        {
            services.AddSingleton<ILocalizationService, ResxLocalizationService>();
            return services;
        }

        public static IServiceCollection AddAppState(this IServiceCollection services) 
        {
            services.AddSingleton<AppState>();
            return services;
        }

        public static IServiceCollection AddApiClients(this IServiceCollection services)
        {
            services.AddHttpClient<IMarketDataProvider, CoinGeckoApiClient>((sp, client) =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var apiKey = config["CoinGecko:ApiKey"];
                var baseUrl = "https://api.coingecko.com/api/v3/";

                client.BaseAddress = new Uri(baseUrl);
                if (!string.IsNullOrWhiteSpace(apiKey))
                    client.DefaultRequestHeaders.Add("x-cg-demo-api-key", apiKey);
            });

            return services;
        }

        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<MainPageViewModel>();
            services.AddTransient<CoinDetailViewModel>();
            services.AddTransient<ConverterViewModel>();
            return services;
        }

        public static IServiceCollection AddViews(this IServiceCollection services)
        {
            services.AddTransient<MainPage>();
            services.AddTransient<ConverterPage>();
            services.AddTransient<CoinDetailPage>();
            services.AddTransient<MainWindow>();
            return services;
        }

        public static IServiceCollection AddNavigation(this IServiceCollection services, Frame frame)
        {
            services.AddSingleton<INavigationManager>(sp =>
            {
                var nav = new FrameNavigationService(sp, frame);
                nav.Register<MainPageViewModel, MainPage>();
                nav.Register<CoinDetailViewModel, CoinDetailPage>();
                nav.Register<ConverterViewModel, ConverterPage>();
                return nav;
            });

            return services;
        }

    }
}
