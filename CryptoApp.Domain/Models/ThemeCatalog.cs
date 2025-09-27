namespace CryptoApp.Domain.Models
{
    public enum Theme
    {
        Dark,
        Light
    }

    public static class ThemeCatalog
    {
        public static readonly Theme[] All = { Theme.Dark, Theme.Light };
    }
}
