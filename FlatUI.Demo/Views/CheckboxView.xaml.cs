using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using FlatUI.Library.Controls;

namespace FlatUI.Demo.Views
{
    public partial class CheckboxView : UserControl
    {
        public CheckboxView()
        {
            InitializeComponent();
        }

        private void ViewSource_Click(object sender, RoutedEventArgs e)
        {
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Views", "CheckboxView.xaml");
            try
            {
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
            }
            catch
            {
                NotificationService.Show("提示", "无法打开源码文件", StatusType.Warning);
            }
        }
    }
}
