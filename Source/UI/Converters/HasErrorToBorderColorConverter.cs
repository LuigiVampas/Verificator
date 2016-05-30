using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace UI.Converters
{
    [ValueConversion(typeof(bool), typeof(SolidColorBrush))]
    public class HasErrorToBorderColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hasError = (bool)value;

            return new SolidColorBrush(hasError ? Colors.Red : Colors.Green);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}