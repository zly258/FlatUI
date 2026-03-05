using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FlatUI.Library.Controls
{
    public enum StatusType
    {
        Default,
        Success,
        Warning,
        Error,
        Info
    }

    public class FlatStatusBadge : System.Windows.Controls.Control
    {
        static FlatStatusBadge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatStatusBadge), new FrameworkPropertyMetadata(typeof(FlatStatusBadge)));
        }

        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(StatusType), typeof(FlatStatusBadge), new PropertyMetadata(StatusType.Default));

        public StatusType Status
        {
            get { return (StatusType)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(FlatStatusBadge), new PropertyMetadata(string.Empty));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }
}