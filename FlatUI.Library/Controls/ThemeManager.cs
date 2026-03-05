using System;
using System.Linq;
using System.Windows;

namespace FlatUI.Library.Controls
{
    public enum ThemeMode
    {
        Light,
        Dark
    }

    public static class ThemeManager
    {
        public static void ChangeTheme(ThemeMode mode)
        {
            var genericDict = System.Windows.Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("FlatUI.Library;component/Themes/Generic.xaml"));

            if (genericDict == null) return;

            var brushesSubDict = genericDict.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Themes/Brushes.xaml"));

            if (brushesSubDict == null) return;

            var source = mode == ThemeMode.Light 
                ? "pack://application:,,,/FlatUI.Library;component/Themes/Themes.Light.xaml"
                : "pack://application:,,,/FlatUI.Library;component/Themes/Themes.Dark.xaml";

            var themeDict = new ResourceDictionary { Source = new Uri(source) };

            brushesSubDict.MergedDictionaries.Clear();
            brushesSubDict.MergedDictionaries.Add(themeDict);
        }

        public static void ChangeAccentColor(System.Windows.Media.Color color)
        {
            var genericDict = System.Windows.Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("FlatUI.Library;component/Themes/Generic.xaml"));

            if (genericDict == null) return;

            var brushesSubDict = genericDict.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Themes/Brushes.xaml"));

            if (brushesSubDict == null) return;

            // 动态修改 PrimaryColor
            brushesSubDict["PrimaryColor"] = color;
            
            // 计算 Hover 和 Pressed 颜色
            brushesSubDict["PrimaryHoverColor"] = System.Windows.Media.Color.FromArgb(
                color.A,
                (byte)Math.Min(255, color.R + 20),
                (byte)Math.Min(255, color.G + 20),
                (byte)Math.Min(255, color.B + 20));

            brushesSubDict["PrimaryPressedColor"] = System.Windows.Media.Color.FromArgb(
                color.A,
                (byte)Math.Max(0, color.R - 20),
                (byte)Math.Max(0, color.G - 20),
                (byte)Math.Max(0, color.B - 20));
        }
    }
}