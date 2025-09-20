using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CryptoApp.Presentation.Converters
{
    public sealed class ChangeToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Brushes.Gray;

            try
            {
                var change = System.Convert.ToDouble(value, CultureInfo.InvariantCulture);
                return change >= 0 ? Brushes.LimeGreen : Brushes.IndianRed;
            }
            catch
            {
                return Brushes.Gray;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => Binding.DoNothing;
    }
}
