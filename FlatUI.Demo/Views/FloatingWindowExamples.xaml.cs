using System.Windows;
using System.Windows.Controls;
using FlatUI.Library.Controls;

namespace FlatUI.Demo.Views
{
    public partial class FloatingWindowExamples : UserControl
    {
        public FloatingWindowExamples()
        {
            InitializeComponent();
        }

        private void OpenFloatingWindow_Click(object sender, RoutedEventArgs e)
        {
            var window = new FlatFloatingWindow
            {
                Width = 300,
                Height = 200,
                Title = "浮动窗口"
            };
            
            var grid = new Grid();
            var textBlock = new TextBlock
            {
                Text = "这是一个浮动窗口",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 14
            };
            grid.Children.Add(textBlock);
            window.Content = grid;
            
            window.Owner = Window.GetWindow(this);
            window.Show();
        }

        private void OpenIconFloatingWindow_Click(object sender, RoutedEventArgs e)
        {
            var window = new FlatFloatingWindow
            {
                Width = 300,
                Height = 200,
                Title = "带图标的浮动窗口",
                IconPathData = "M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm-2 15l-5-5 1.41-1.41L10 14.17l7.59-7.59L19 8l-9 9z"
            };
            
            var grid = new Grid();
            var textBlock = new TextBlock
            {
                Text = "这是一个带图标的浮动窗口",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 14
            };
            grid.Children.Add(textBlock);
            window.Content = grid;
            
            window.Owner = Window.GetWindow(this);
            window.Show();
        }

        private void OpenTopLeft_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this);
            var window = new FlatFloatingWindow
            {
                Width = 250,
                Height = 150,
                Title = "左上角",
                Left = mainWindow.Left + 50,
                Top = mainWindow.Top + 100
            };
            
            var grid = new Grid();
            var textBlock = new TextBlock
            {
                Text = "左上角浮动窗口",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 14
            };
            grid.Children.Add(textBlock);
            window.Content = grid;
            
            window.Owner = mainWindow;
            window.Show();
        }

        private void OpenTopRight_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this);
            var window = new FlatFloatingWindow
            {
                Width = 250,
                Height = 150,
                Title = "右上角",
                Left = mainWindow.Left + mainWindow.Width - 300,
                Top = mainWindow.Top + 100
            };
            
            var grid = new Grid();
            var textBlock = new TextBlock
            {
                Text = "右上角浮动窗口",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 14
            };
            grid.Children.Add(textBlock);
            window.Content = grid;
            
            window.Owner = mainWindow;
            window.Show();
        }

        private void OpenBottomLeft_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this);
            var window = new FlatFloatingWindow
            {
                Width = 250,
                Height = 150,
                Title = "左下角",
                Left = mainWindow.Left + 50,
                Top = mainWindow.Top + mainWindow.Height - 250
            };
            
            var grid = new Grid();
            var textBlock = new TextBlock
            {
                Text = "左下角浮动窗口",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 14
            };
            grid.Children.Add(textBlock);
            window.Content = grid;
            
            window.Owner = mainWindow;
            window.Show();
        }

        private void OpenBottomRight_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this);
            var window = new FlatFloatingWindow
            {
                Width = 250,
                Height = 150,
                Title = "右下角",
                Left = mainWindow.Left + mainWindow.Width - 300,
                Top = mainWindow.Top + mainWindow.Height - 250
            };
            
            var grid = new Grid();
            var textBlock = new TextBlock
            {
                Text = "右下角浮动窗口",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 14
            };
            grid.Children.Add(textBlock);
            window.Content = grid;
            
            window.Owner = mainWindow;
            window.Show();
        }
    }
}
