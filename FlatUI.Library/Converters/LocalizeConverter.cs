using System;
using System.Globalization;
using System.Windows.Data;
using FlatUI.Library.Controls;

namespace FlatUI.Library.Converters
{
    /// <summary>
    /// 本地化转换器
    /// </summary>
    public class LocalizeConverter : IValueConverter
    {
        public static LocalizeConverter Instance { get; } = new LocalizeConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string key)
            {
                return LanguageManager.Instance.GetString(key);
            }
            return value?.ToString() ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}