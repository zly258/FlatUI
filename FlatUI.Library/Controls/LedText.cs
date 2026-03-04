using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FlatUI.Library.Controls
{
    public class LedText : System.Windows.Controls.Control
    {
        static LedText()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LedText), new FrameworkPropertyMetadata(typeof(LedText)));
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(LedText), new PropertyMetadata("00:00"));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty LedColorProperty =
            DependencyProperty.Register("LedColor", typeof(System.Windows.Media.Brush), typeof(LedText), new PropertyMetadata(new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 255, 0))));

        public System.Windows.Media.Brush LedColor
        {
            get { return (System.Windows.Media.Brush)GetValue(LedColorProperty); }
            set { SetValue(LedColorProperty, value); }
        }
    }
}