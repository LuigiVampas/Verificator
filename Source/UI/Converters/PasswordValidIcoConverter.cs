using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Presentation.Validation;

namespace UI.Converters
{
    [ValueConversion(typeof (PasswordStrength), typeof (BitmapSource))]
    public class PasswordValidIcoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var passwordStrength = (PasswordStrength)value;
            Bitmap bmp;

            switch (passwordStrength)
            {
                case PasswordStrength.PasswordNotSet:
                    bmp = ImageResources.ImageResources.Exclamation;
                    break;
                case PasswordStrength.Weak:
                    bmp = ImageResources.ImageResources.Exclamation;
                    break;
                case PasswordStrength.Normal:
                    bmp = ImageResources.ImageResources.Check;
                    break;
                case PasswordStrength.Strong:
                    bmp = ImageResources.ImageResources.Check;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var screenCapture = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
               bmp.GetHbitmap(),
               IntPtr.Zero,
               System.Windows.Int32Rect.Empty,
               BitmapSizeOptions.FromWidthAndHeight(20, 20));

            return screenCapture;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}