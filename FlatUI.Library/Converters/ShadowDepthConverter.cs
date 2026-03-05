using System;
using System.Globalization;
using System.Windows.Data;
using FlatUI.Library.Controls;

namespace FlatUI.Library.Converters
{
    /// <summary>
    /// 阴影深度转换器
    /// </summary>
    public class ShadowDepthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ShadowDepth depth)
            {
                return depth switch
                {
                    ShadowDepth.None => 0,
                    ShadowDepth.Small => 5,
                    ShadowDepth.Medium => 10,
                    ShadowDepth.Large => 15,
                    _ => 10
                };
            }
            return 10;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 阴影深度到阴影距离转换器
    /// </summary>
    public class ShadowDepthToShadowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ShadowDepth depth)
            {
                return depth switch
                {
                    ShadowDepth.None => 0,
                    ShadowDepth.Small => 2,
                    ShadowDepth.Medium => 3,
                    ShadowDepth.Large => 4,
                    _ => 3
                };
            }
            return 3;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}