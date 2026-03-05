using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FlatUI.Library.Controls;

namespace FlatUI.Demo.Views
{
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            
            // 初始化语言选择器
            InitializeLanguageSelector();
        }

        private void InitializeLanguageSelector()
        {
            // 根据当前语言设置选中项
            var currentLang = LanguageManager.Instance.CurrentCulture.Name;
            foreach (var item in LanguageCombo.Items)
            {
                if (item is ComboBoxItem comboItem)
                {
                    if (comboItem.Tag?.ToString() == currentLang)
                    {
                        LanguageCombo.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private void LanguageCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LanguageCombo.SelectedItem is ComboBoxItem item)
            {
                var lang = item.Tag?.ToString();
                if (!string.IsNullOrEmpty(lang))
                {
                    if (lang == "zh-CN")
                    {
                        LanguageManager.Instance.SwitchToChinese();
                    }
                    else if (lang == "en-US")
                    {
                        LanguageManager.Instance.SwitchToEnglish();
                    }
                    
                    // 提示用户 - 暂时不显示通知，避免类型冲突
                    // NotificationService.Show("语言已切换 / Language switched", 
                    //     lang == "zh-CN" ? "已切换到中文" : "Switched to English", 
                    //     type: FlatUI.Library.Controls.NotificationType.Info);
                }
            }
        }

        private void LightMode_Click(object sender, RoutedEventArgs e) => ThemeManager.ChangeTheme(ThemeMode.Light);
        private void DarkMode_Click(object sender, RoutedEventArgs e) => ThemeManager.ChangeTheme(ThemeMode.Dark);

        private void Accent_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string hex)
            {
                var color = (Color)ColorConverter.ConvertFromString(hex);
                ThemeManager.ChangeAccentColor(color);
            }
        }
    }
}