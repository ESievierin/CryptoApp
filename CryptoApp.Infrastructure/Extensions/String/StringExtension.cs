namespace CryptoApp.Infrastructure.Extensions.String
{
    public static class StringExtensions
    {
        private const string NA = "N/A";

        public static string OrNA(this string? value) =>
            string.IsNullOrWhiteSpace(value) ? NA : value;
    }
}
