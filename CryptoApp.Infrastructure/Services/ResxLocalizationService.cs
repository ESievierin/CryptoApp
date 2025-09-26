using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Threading;
using CryptoApp.Domain.Models;
using CryptoApp.Domain.Services;

namespace CryptoApp.Infrastructure.Services
{
    public class ResxLocalizationService : ILocalizationService, INotifyPropertyChanged
    {
        private readonly ResourceManager resourceManager;
        private Language currentLanguage = LanguageCatalog.English;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Language CurrentLanguage
        {
            get => currentLanguage;
            private set
            {
                if (currentLanguage.Code != value.Code)
                {
                    currentLanguage = value;
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(value.Code);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentLanguage)));
                }
            }
        }

        public ResxLocalizationService(ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(currentLanguage.Code);
        }

        public void SetLanguage(Language language)
        {
            CurrentLanguage = language;
        }

        public string Translate(string key) =>
            resourceManager.GetString(key) ?? key;
    }
}
