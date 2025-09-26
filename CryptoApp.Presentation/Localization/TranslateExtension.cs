using CryptoApp.Domain.Services;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace CryptoApp.Presentation.Localization
{
    [MarkupExtensionReturnType(typeof(string))]
    public class TranslateExtension : MarkupExtension
    {
        public string Key { get; set; }

        public TranslateExtension(string key) => Key = key;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return Key;

            var binding = new Binding(nameof(ILocalizationService.CurrentLanguage))
            {
                Source = LocalizationProvider.Service,
                Mode = BindingMode.OneWay,
                Converter = new TranslateConverter(Key)
            };

            return binding.ProvideValue(serviceProvider);
        }

        private sealed class TranslateConverter(string key) : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
                => LocalizationProvider.Service?.Translate(key) ?? key;

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
                => Binding.DoNothing;
        }
    }

}
