using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FlatUI.Library.Controls;
using FlatUI.Demo.Views;

namespace FlatUI.Demo
{
    public partial class MainWindow : FlatWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            
            DataContext = this;
            Loaded += (s, e) => {
                NotificationService.RegisterHost(this);
                TrayManager.Initialize("FlatUI Demo", System.Drawing.SystemIcons.Application);
                 
                 var menu = new System.Windows.Forms.ContextMenuStrip();
                 menu.Items.Add("Show MainWindow", null, (s, e) => {
                     this.Show();
                     this.WindowState = WindowState.Normal;
                     this.Activate();
                 });
                 menu.Items.Add("Exit", null, (s, e) => Application.Current.Shutdown());
                 TrayManager.SetContextMenu(menu);

                 // 初始化默认视图
                 MainContent.Content = new DashboardView();
             };
            
            Closed += (s, e) => TrayManager.Dispose();
        }

        private void NavList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NavList?.SelectedItem is not ListBoxItem item || MainContent == null)
                return;
                
            var tag = item.Tag?.ToString();
            if (ViewTitle != null) ViewTitle.Text = GetViewTitle(tag);
            
            switch (tag)
            {
                case "Dashboard":
                    MainContent.Content = new DashboardView();
                    break;
                case "Chart":
                    MainContent.Content = new ChartView();
                    break;
                case "Avatar":
                    MainContent.Content = new AvatarView();
                    break;
                case "Badge":
                    MainContent.Content = new BadgeView();
                    break;
                case "Tag":
                    MainContent.Content = new TagView();
                    break;
                case "Bubble":
                    MainContent.Content = new BubbleView();
                    break;
                case "Settings":
                    MainContent.Content = new SettingsView();
                    break;
            }
        }

        private string GetViewTitle(string? tag)
        {
            // 使用 LanguageManager 获取本地化的标题
            if (string.IsNullOrEmpty(tag)) return "";
            
            switch (tag)
            {
                case "Dashboard": return LanguageManager.Instance.GetString("Nav_Dashboard");
                case "Chart": return LanguageManager.Instance.GetString("Nav_Chart");
                case "Avatar": return LanguageManager.Instance.GetString("Nav_Avatar");
                case "Badge": return LanguageManager.Instance.GetString("Nav_Badge");
                case "Tag": return LanguageManager.Instance.GetString("Nav_Tag");
                case "Bubble": return LanguageManager.Instance.GetString("Nav_Bubble");
                case "Settings": return LanguageManager.Instance.GetString("Nav_Settings");
                default: return tag ?? "";
            }
        }

        private void LanguageSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LanguageSelector.SelectedItem is ComboBoxItem item)
            {
                var lang = item.Tag?.ToString();
                if (!string.IsNullOrEmpty(lang))
                {
                    // 切换语言
                    if (lang == "zh-CN")
                    {
                        LanguageManager.Instance.SwitchToChinese();
                        // 更新当前选中项的显示文本
                        item.Content = "简体中文";
                    }
                    else if (lang == "en-US")
                    {
                        LanguageManager.Instance.SwitchToEnglish();
                        // 更新当前选中项的显示文本
                        item.Content = "English";
                    }
                    
                    // 刷新当前视图标题
                    if (NavList.SelectedItem is ListBoxItem selected)
                    {
                        var tag = selected.Tag?.ToString();
                        if (ViewTitle != null) ViewTitle.Text = GetViewTitle(tag);
                    }
                }
            }
        }

        private bool _isCollapsed = false;
        private void CollapseNav_Click(object sender, RoutedEventArgs e)
        {
            _isCollapsed = !_isCollapsed;
            SideColumn.Width = _isCollapsed ? new GridLength(60) : new GridLength(220);
            LogoText.Visibility = _isCollapsed ? Visibility.Collapsed : Visibility.Visible;
            if (sender is Button btn)
            {
                btn.Content = _isCollapsed ? ">" : "Collapse";
            }
        }

        private void Notify_Click(object sender, RoutedEventArgs e)
        {
            NotificationService.Show("System Notification", "You have a new message from the dashboard.", StatusType.Info);
        }

        private void Screenshot_Click(object sender, RoutedEventArgs e)
        {
            var bmp = ScreenshotService.CaptureScreen();
            NotificationService.Show("Screenshot", "Screen captured successfully!", StatusType.Success);
        }

        private void FlashTray_v2_Click(object sender, RoutedEventArgs e) => TrayManager.StartFlashing();
        private void SystemNotify_v2_Click(object sender, RoutedEventArgs e) => NotificationService.ShowSystem("FlatUI", "System notification sent!");
        private void CloseDrawer_Click(object sender, RoutedEventArgs e) => SideDrawer.IsOpen = false;
    }

    public class DemoItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public double Value { get; set; }
    }

    public class CardItem
    {
        public string Title { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Progress { get; set; }
    }
}