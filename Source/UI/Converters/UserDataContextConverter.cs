﻿using System;
using System.Globalization;
using System.Windows.Data;
using Presentation.Contexts;

namespace UI.Converters
{
    [ValueConversion(typeof(object), typeof(UserDataContext))]
    public class UserDataContextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (UserDataContext) value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}