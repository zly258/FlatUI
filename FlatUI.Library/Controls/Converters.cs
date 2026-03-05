using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Point = System.Windows.Point;

namespace FlatUI.Library.Controls
{
    /// <summary>
    /// 窗口状态到命令转换器
    /// </summary>
    public class WindowStateToCommandConverter : IValueConverter
    {
        public static readonly WindowStateToCommandConverter Instance = new WindowStateToCommandConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WindowState state)
            {
                return state == WindowState.Maximized ? SystemCommands.RestoreWindowCommand : SystemCommands.MaximizeWindowCommand;
            }
            return SystemCommands.MaximizeWindowCommand;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 形状到圆角转换器
    /// </summary>
    public class ShapeToCornerRadiusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is AvatarShape shape)
            {
                return shape == AvatarShape.Circle ? new CornerRadius(double.PositiveInfinity) : new CornerRadius(8);
            }
            return new CornerRadius(8);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 宽度到裁剪转换器
    /// </summary>
    public class WidthToClipConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double width)
            {
                return new System.Windows.Media.RectangleGeometry(new Rect(0, 0, width, width));
            }
            return new System.Windows.Media.RectangleGeometry();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Null 到布尔转换器
    /// </summary>
    public class NullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Null 到可见性转换器
    /// </summary>
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 布尔到可见性转换器（带反向参数）
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                // 如果有 Inverted 参数，反转结果
                if (parameter is string str && str == "Inverted")
                {
                    return boolValue ? Visibility.Collapsed : Visibility.Visible;
                }
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 双倍到一半转换器（用于圆形计算）
    /// </summary>
    public class DoubleToHalfConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double doubleValue)
            {
                return doubleValue / 2;
            }
            return 0.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 进度条宽度转换器
    /// </summary>
    public class ProgressWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length >= 4 && 
                values[0] is double trackWidth && 
                values[1] is double currentValue && 
                values[2] is double minimum && 
                values[3] is double maximum)
            {
                if (maximum <= minimum) return 0.0;
                
                var percentage = (currentValue - minimum) / (maximum - minimum);
                return Math.Max(0, Math.Min(trackWidth, trackWidth * percentage));
            }
            return 0.0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 进度百分比转换器
    /// </summary>
    public class ProgressPercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double currentValue)
            {
                return $"{Math.Round(currentValue)}%";
            }
            return "0%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 进度类型到可见性转换器
    /// </summary>
    public class ProgressTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ProgressType currentType && parameter is ProgressType targetProgressType)
            {
                return currentType == targetProgressType ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 环形进度条角度转换器
    /// </summary>
    public class CircularProgressAngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double currentValue)
            {
                // 将进度值转换为角度 (0-360)
                var percentage = Math.Max(0, Math.Min(1, currentValue / 100.0));
                return percentage * 360;
            }
            return 0.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 环形进度条点转换器
    /// </summary>
    public class CircularProgressPointConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length >= 2 && 
                values[0] is double angle && 
                values[1] is double radius)
            {
                var radians = angle * Math.PI / 180;
                var x = 50 + radius * Math.Sin(radians);
                var y = 50 - radius * Math.Cos(radians);
                return new Point(x, y);
            }
            return new Point(50, 10);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
