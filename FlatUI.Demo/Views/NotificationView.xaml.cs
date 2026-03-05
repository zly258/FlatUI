using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using FlatUI.Library.Controls;

namespace FlatUI.Demo.Views
{
    public partial class NotificationView : UserControl
    {
        public NotificationView()
        {
            InitializeComponent();
        }

        private void ViewSource_Click(object sender, RoutedEventArgs e)
        {
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Views", "NotificationView.xaml");
            try
            {
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
            }
            catch
            {
                NotificationService.Show("提示", "无法打开源码文件", StatusType.Warning);
            }
        }

        private void ShowInfo_Click(object sender, RoutedEventArgs e)
        {
            NotificationService.Show("信息", "这是一条信息通知。", StatusType.Info);
        }

        private void ShowSuccess_Click(object sender, RoutedEventArgs e)
        {
            NotificationService.Show("成功", "操作已成功完成！", StatusType.Success);
        }

        private void ShowWarning_Click(object sender, RoutedEventArgs e)
        {
            NotificationService.Show("警告", "请检查输入数据。", StatusType.Warning);
        }

        private void ShowError_Click(object sender, RoutedEventArgs e)
        {
            NotificationService.Show("错误", "处理过程中发生错误。", StatusType.Error);
        }

        private void ShowSystem_Click(object sender, RoutedEventArgs e)
        {
            NotificationService.ShowSystem("FlatUI 演示", "这是一条系统通知！");
        }
    }
}
