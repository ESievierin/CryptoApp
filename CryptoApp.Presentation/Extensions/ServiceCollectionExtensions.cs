using CryptoApp.ApplicationCore.ViewModels;
using CryptoApp.Domain.Services;
using CryptoApp.Infrastructure.Mappers;
using CryptoApp.Infrastructure.Services;
using CryptoApp.Presentation.Pages;
using CryptoApp.Presentation.Services.Navigation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<CryptoAppMapper>();
            }, typeof(CryptoAppMapper).Assembly);

            return services;
        }

        public static IServiceCollection AddApiClients(this IServiceCollection services)
        {
            services.AddHttpClient<IMarketDataProvider, CoinGeckoApiClient>((sp, client) =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var apiKey = config["CoinGecko:ApiKey"];
                var BaseUrl = "https://api.coingecko.com/api/v3/";

                client.BaseAddress = new Uri(BaseUrl);
                if (!string.IsNullOrWhiteSpace(apiKey))
                    client.DefaultRequestHeaders.Add("x-cg-demo-api-key", apiKey);
            });

            return services;
        }

        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<MainPageViewModel>();
            return services;
        }

        public static IServiceCollection AddViews(this IServiceCollection services)
        {
            services.AddTransient<MainPage>();
            services.AddTransient<MainWindow>();
            return services;
        }

        public static IServiceCollection AddNavigation(this IServiceCollection services)
        {
            services.AddSingleton<INavigationService>(sp =>
            {
                var mainWindow = sp.GetRequiredService<MainWindow>();
                var nav = new FrameNavigationService(sp, mainWindow.MainFrame);

                nav.Register<MainPageViewModel, MainPage>();
                return nav;
            });

            return services;

        }
    }
}
