using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace FlatUI.Library.Controls
{
    public enum ThemeMode
    {
        Light,
        Dark
    }

    public static class ThemeManager
    {
        public static event EventHandler? ThemeChanged;

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

            NotifyThemeChanged();
        }

        public static void ChangeAccentColor(System.Windows.Media.Color color)
        {
            var genericDict = System.Windows.Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("FlatUI.Library;component/Themes/Generic.xaml"));

            if (genericDict == null) return;

            var brushesSubDict = genericDict.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Themes/Brushes.xaml"));

            if (brushesSubDict == null) return;

            brushesSubDict["PrimaryColor"] = color;
            
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

            NotifyThemeChanged();
        }

        private static void NotifyThemeChanged()
        {
            ThemeChanged?.Invoke(null, EventArgs.Empty);

            foreach (Window window in System.Windows.Application.Current.Windows)
            {
                // 强制刷新窗口资源
                window.Resources["PrimaryBrush"] = System.Windows.Application.Current.FindResource("PrimaryBrush");
                window.Resources["PrimaryHoverBrush"] = System.Windows.Application.Current.FindResource("PrimaryHoverBrush");
                window.Resources["PrimaryPressedBrush"] = System.Windows.Application.Current.FindResource("PrimaryPressedBrush");
                window.Resources["PrimaryLightBrush"] = System.Windows.Application.Current.FindResource("PrimaryLightBrush");
                
                // 强制刷新窗口标题栏背景色
                if (window is FlatWindow flatWindow)
                {
                    // 强制重新应用窗口模板
                    var template = flatWindow.Template;
                    flatWindow.Template = null;
                    flatWindow.Template = template;
                }
            }
        }
    }
}