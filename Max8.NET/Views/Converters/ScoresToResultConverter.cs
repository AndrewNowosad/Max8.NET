using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Max8.NET.Views.Converters
{
    class ScoresToResultConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Contains(DependencyProperty.UnsetValue))
                return DependencyProperty.UnsetValue;
            var p1 = (int)values[0];
            var p2 = (int)values[1];
            if (p1 > p2) return "Победил первый игрок!";
            if (p1 < p2) return "Победил второй игрок!";
            return "Ничья!";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
