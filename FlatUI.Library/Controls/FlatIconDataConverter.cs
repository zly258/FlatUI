using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace FlatUI.Library.Controls
{
    /// <summary>
    /// 图标数据转换器
    /// </summary>
    public class IconDataConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is FlatIcon icon)
            {
                return icon.GetIconData();
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}