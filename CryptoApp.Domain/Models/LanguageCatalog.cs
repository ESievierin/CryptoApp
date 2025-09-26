namespace CryptoApp.Domain.Models
{
    public record Language(string Code, string DisplayName);

    public static class LanguageCatalog
    {
        public static readonly Language English = new("en", "English");
        public static readonly Language Ukrainian = new("uk", "Українська");
        public static readonly Language Spanish = new("es", "Español");

        public static readonly Language[] All =
        {
            English, Ukrainian, Spanish
        };
    }
}

