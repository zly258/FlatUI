using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FlatUI.Library.Controls
{
    /// <summary>
    /// LED文本控件
    /// </summary>
    public class FlatLedText : System.Windows.Controls.Control
    {
        static FlatLedText()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatLedText), new FrameworkPropertyMetadata(typeof(FlatLedText)));
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(FlatLedText), new PropertyMetadata("00:00"));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty LedColorProperty =
            DependencyProperty.Register("LedColor", typeof(System.Windows.Media.Brush), typeof(FlatLedText), new PropertyMetadata(new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 255, 0))));

        public System.Windows.Media.Brush LedColor
        {
            get { return (System.Windows.Media.Brush)GetValue(LedColorProperty); }
            set { SetValue(LedColorProperty, value); }
        }
    }
}