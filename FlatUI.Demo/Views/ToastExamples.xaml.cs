using System.Windows;
using System.Windows.Controls;

namespace FlatUI.Demo.Views
{
    public partial class ToastExamples : UserControl
    {
        public ToastExamples()
        {
            InitializeComponent();
        }

        private void SuccessToast_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("成功提示消息", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void InfoToast_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("信息提示消息", "信息", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void WarningToast_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("警告提示消息", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ErrorToast_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("错误提示消息", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void TopToast_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("顶部位置消息", "位置", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BottomToast_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("底部位置消息", "位置", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LeftToast_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("左侧位置消息", "位置", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RightToast_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("右侧位置消息", "位置", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ShortToast_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("2秒后自动关闭", "时长", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MediumToast_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("5秒后自动关闭", "时长", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LongToast_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("10秒后自动关闭", "时长", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
