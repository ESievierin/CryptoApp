using System.Globalization;
using System.Windows.Data;

namespace CryptoApp.Presentation.Converters
{
    public sealed class TargetCurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string pair && pair.Contains("/"))
            {
                var parts = pair.Split('/');

                if (parts.Length == 2)
                    return parts[1];
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => Binding.DoNothing;
    }
}
