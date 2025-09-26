using CryptoApp.Domain.Services;

namespace CryptoApp.Presentation.Localization
{
    public static class LocalizationProvider
    {
        public static ILocalizationService? Service { get; private set; }
        public static event EventHandler? Initialized;

        public static void Initialize(ILocalizationService service)
        {
            Service = service;
            Initialized?.Invoke(null, EventArgs.Empty);
        }
    }

}
