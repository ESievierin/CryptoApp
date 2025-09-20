using System.Globalization;
using System.Windows.Data;

namespace CryptoApp.Presentation.Converters
{
    public sealed class ToUpperConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString()?.ToUpper();
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }
    }
}
