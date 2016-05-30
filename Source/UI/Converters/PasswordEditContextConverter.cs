using System;
using System.Globalization;
using System.Windows.Data;
using Presentation.Contexts;

namespace UI.Converters
{
    [ValueConversion(typeof(object), typeof(PasswordEditContext))]
    public class PasswordEditContextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (PasswordEditContext)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}