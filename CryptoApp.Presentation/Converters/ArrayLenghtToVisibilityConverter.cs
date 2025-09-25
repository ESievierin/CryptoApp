using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CryptoApp.Presentation.Converters
{
    public class ArrayLenghtToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Array arr && arr.Length > 0)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => Binding.DoNothing;
    }
}
