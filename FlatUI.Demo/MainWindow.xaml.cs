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
                case "仪表台":
                    MainContent.Content = new DashboardView();
                    break;
                case "图表":
                    MainContent.Content = new ChartView();
                    break;
                case "表格":
                    MainContent.Content = new DataDisplayView();
                    break;
                case "列表":
                    MainContent.Content = new Views.ListView();
                    break;
                case "树形":
                    MainContent.Content = new Views.TreeView();
                    break;
                case "按钮":
                    MainContent.Content = new ButtonView();
                    break;
                case "输入框":
                    MainContent.Content = new InputView();
                    break;
                case "复选框":
                    MainContent.Content = new CheckboxView();
                    break;
                case "头像":
                    MainContent.Content = new AvatarView();
                    break;
                case "徽标":
                    MainContent.Content = new BadgeView();
                    break;
                case "标签":
                    MainContent.Content = new TagView();
                    break;
                case "气泡":
                    MainContent.Content = new BubbleView();
                    break;
                case "通知":
                    MainContent.Content = new NotificationView();
                    break;
                case "抽屉":
                    MainContent.Content = new DrawerView();
                    break;
                case "分页":
                    MainContent.Content = new PaginationView();
                    break;
                case "数字输入":
                    MainContent.Content = new NumericUpDownView();
                    break;
                case "下拉树":
                    MainContent.Content = new DropdownTreeView();
                    break;
                case "下拉表格":
                    MainContent.Content = new DropdownGridView();
                    break;
                case "设置":
                    MainContent.Content = new SettingsView();
                    break;
            }
        }

        private string GetViewTitle(string? tag)
        {
            if (string.IsNullOrEmpty(tag)) return "";
            
            switch (tag)
            {
                case "仪表台": return "仪表台";
                case "图表": return "图表";
                case "表格": return "表格";
                case "列表": return "列表";
                case "树形": return "树形";
                case "按钮": return "按钮";
                case "输入框": return "输入框";
                case "复选框": return "复选框";
                case "头像": return "头像";
                case "徽标": return "徽标";
                case "标签": return "标签";
                case "气泡": return "气泡";
                case "通知": return "通知";
                case "抽屉": return "抽屉";
                case "分页": return "分页";
                case "数字输入": return "数字输入";
                case "下拉树": return "下拉树";
                case "下拉表格": return "下拉表格";
                case "设置": return "设置";
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