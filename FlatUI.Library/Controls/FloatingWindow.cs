using System.Windows;
using System.Windows.Input;

namespace FlatUI.Library.Controls
{
    public class FloatingWindow : Window
    {
        static FloatingWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FloatingWindow), new FrameworkPropertyMetadata(typeof(FloatingWindow)));
        }

        public FloatingWindow()
        {
            WindowStyle = WindowStyle.None;
            AllowsTransparency = true;
            Background = System.Windows.Media.Brushes.Transparent;
            Topmost = true;
            ShowInTaskbar = false;
            
            MouseLeftButtonDown += (s, e) => { if (e.LeftButton == MouseButtonState.Pressed) DragMove(); };
        }

        public static readonly DependencyProperty IconPathDataProperty =
            DependencyProperty.Register("IconPathData", typeof(string), typeof(FloatingWindow), new PropertyMetadata(null));

        public string IconPathData
        {
            get { return (string)GetValue(IconPathDataProperty); }
            set { SetValue(IconPathDataProperty, value); }
        }
    }
}