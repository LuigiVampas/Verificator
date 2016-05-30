using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace UI.Converters
{
    [ValueConversion(typeof (bool), typeof (BitmapSource))]
    public class HasErrorToBmpConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hasError = (bool)value;
            var bmp = hasError 
                ? ImageResources.ImageResources.Exclamation
                : ImageResources.ImageResources.Check;

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