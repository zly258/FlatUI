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
            var brushesDict = System.Windows.Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("FlatUI.Library;component/Themes/Generic.xaml"));

            if (brushesDict == null) return;

            // In our structure, Generic.xaml merges Brushes.xaml
            // Brushes.xaml merges Themes.Light.xaml or Themes.Dark.xaml
            // We need to find Brushes.xaml inside Generic.xaml
            var genericDict = brushesDict;
            var brushesSubDict = genericDict.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Themes/Brushes.xaml"));

            if (brushesSubDict == null) return;

            var source = mode == ThemeMode.Light 
                ? "pack://application:,,,/FlatUI.Library;component/Themes/Themes.Light.xaml"
                : "pack://application:,,,/FlatUI.Library;component/Themes/Themes.Dark.xaml";

            var themeDict = new ResourceDictionary { Source = new Uri(source) };

            // Replace the theme colors
            brushesSubDict.MergedDictionaries.Clear();
            brushesSubDict.MergedDictionaries.Add(themeDict);
        }
    }
}