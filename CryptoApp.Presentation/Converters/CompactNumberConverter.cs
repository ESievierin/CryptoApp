using System.Globalization;
using System.Windows.Data;

namespace CryptoApp.Presentation.Converters
{
    public sealed class CompactNumberConverter : IValueConverter
    {
        private const string DefaultFormat = "N0";
        private const string ScaledFormat = "F2";

        private static readonly IReadOnlyList<(decimal Threshold, string Suffix)> FormatRules = new List<(decimal, string)>
        {
            (1_000_000_000_000M, "T"),
            (1_000_000_000M, "B"),
            (1_000_000M, "M")
        }.AsReadOnly();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not decimal cap)
                return value;

            foreach (var (threshold, suffix) in FormatRules)
                if (cap >= threshold)
                   return $"{(cap / threshold).ToString(ScaledFormat, culture)}{suffix}";

            return cap.ToString(DefaultFormat, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => Binding.DoNothing;
    }
}