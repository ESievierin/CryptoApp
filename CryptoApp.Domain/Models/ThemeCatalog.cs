namespace CryptoApp.Domain.Models
{
    public enum Theme
    {
        Dark,
        Light
    }

    public static class ThemeCatalog
    {
        public static readonly IReadOnlyList<Theme> All = Enum.GetValues<Theme>();
    }
}
