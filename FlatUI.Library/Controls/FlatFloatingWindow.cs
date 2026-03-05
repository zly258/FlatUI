using System.Windows;
using System.Windows.Input;

namespace FlatUI.Library.Controls
{
    /// <summary>
    /// 浮动窗口控件
    /// </summary>
    public class FlatFloatingWindow : Window
    {
        static FlatFloatingWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatFloatingWindow), new FrameworkPropertyMetadata(typeof(FlatFloatingWindow)));
        }

        public FlatFloatingWindow()
        {
            WindowStyle = WindowStyle.None;
            AllowsTransparency = true;
            Background = System.Windows.Media.Brushes.Transparent;
            Topmost = true;
            ShowInTaskbar = false;
            
            MouseLeftButtonDown += (s, e) => { if (e.LeftButton == MouseButtonState.Pressed) DragMove(); };
        }

        public static readonly DependencyProperty IconPathDataProperty =
            DependencyProperty.Register("IconPathData", typeof(string), typeof(FlatFloatingWindow), new PropertyMetadata(null));

        public string IconPathData
        {
            get { return (string)GetValue(IconPathDataProperty); }
            set { SetValue(IconPathDataProperty, value); }
        }
    }
}