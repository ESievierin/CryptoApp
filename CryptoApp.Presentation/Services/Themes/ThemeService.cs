using CryptoApp.Domain.Models;
using CryptoApp.Domain.Services;
using System.Windows;

namespace CryptoApp.Presentation.Services
{
    public class ThemeService : IThemeService
    {
        public void ApplyTheme(Theme theme)
        {
            var appResources = Application.Current.Resources.MergedDictionaries;
            var themeBasePath = "Resources/Themes";
            var existingTheme = appResources.FirstOrDefault(d =>
                d.Source != null && d.Source.OriginalString.Contains($"{themeBasePath}/"));

            if (existingTheme != null)
                appResources.Remove(existingTheme);

            var newTheme = new ResourceDictionary
            {
                Source = new Uri(
                    theme == Theme.Dark
                        ? $"{themeBasePath}/DarkTheme.xaml"
                        : $"{themeBasePath}/LightTheme.xaml",
                    UriKind.Relative)
            };

            appResources.Add(newTheme);
        }

    }
}
