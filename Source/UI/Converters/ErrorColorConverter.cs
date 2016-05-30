using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Presentation.Validation;

namespace UI.Converters
{
    [ValueConversion(typeof(PasswordStrength), typeof(SolidColorBrush))]
    public class ErrorColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var passwordStrength = (PasswordStrength) value;

            switch (passwordStrength)
            {
                case PasswordStrength.PasswordNotSet :
                    return new SolidColorBrush(Colors.Red);
                case PasswordStrength.Weak:
                    return new SolidColorBrush(Colors.Red);
                case PasswordStrength.Normal:
                    return new SolidColorBrush(Colors.Yellow);
                case PasswordStrength.Strong:
                    return new SolidColorBrush(Colors.Green);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
