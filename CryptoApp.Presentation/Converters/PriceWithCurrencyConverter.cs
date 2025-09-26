using CryptoApp.Domain.Models;
using System.Globalization;
using System.Windows.Data;

namespace CryptoApp.Presentation.Converters
{
    public sealed class PriceWithCurrencyConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2) return string.Empty;

            if (values[0] is decimal price && values[1] is Currency currency)
            {
                return $"{currency.Symbol}{price:N2}";
            }

            return string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => targetTypes.Select(_ => Binding.DoNothing).ToArray();
    }
}
