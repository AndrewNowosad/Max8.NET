using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Max8.NET.Views.Converters
{
    class BoolConverter : IValueConverter
    {
        public object TrueValue { get; set; }
        public object FalseValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
                return b ? TrueValue : FalseValue;
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Equals(value, TrueValue)) return true;
            if (Equals(value, FalseValue)) return false;
            return DependencyProperty.UnsetValue;
        }
    }
}