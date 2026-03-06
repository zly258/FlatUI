using System;
using System.Globalization;
using System.Windows.Data;

namespace FlatUI.Demo.Converters
{
    public class BooleanToAngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? 180.0 : 0.0;
            }
            return 0.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double angle)
            {
                return angle >= 90.0;
            }
            return false;
        }
    }
}
