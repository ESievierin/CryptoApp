using CryptoApp.Domain.Models;
using System.ComponentModel;

namespace CryptoApp.Domain.Services
{
    public interface ILocalizationService
    {
        Language CurrentLanguage { get; }
        void SetLanguage(Language language);
        string Translate(string key);

        event PropertyChangedEventHandler? PropertyChanged;
    }
}
