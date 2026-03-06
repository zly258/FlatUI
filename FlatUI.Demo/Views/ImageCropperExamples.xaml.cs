using System.Windows;
using System.Windows.Controls;

namespace FlatUI.Demo.Views
{
    public partial class ImageCropperExamples : UserControl
    {
        public ImageCropperExamples()
        {
            InitializeComponent();
        }

        private void OpenCropper_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("打开图片裁剪器", "图片裁剪", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Crop1x1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("1:1 比例裁剪", "图片裁剪", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Crop4x3_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("4:3 比例裁剪", "图片裁剪", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Crop16x9_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("16:9 比例裁剪", "图片裁剪", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RotateLeft_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("左旋转 90 度", "图片裁剪", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RotateRight_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("右旋转 90 度", "图片裁剪", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void FlipHorizontal_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("水平翻转", "图片裁剪", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
