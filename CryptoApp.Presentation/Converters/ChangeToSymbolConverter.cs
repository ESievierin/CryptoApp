using System.Globalization;
using System.Windows.Data;

namespace CryptoApp.Presentation.Converters
{
    public sealed class ChangeToSymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal change)
                return change >= 0 ? "↑" : "↓";
            return "-";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
            => Binding.DoNothing;
    }
}
