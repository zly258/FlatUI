using System.Windows;
using System.Windows.Controls;

namespace FlatUI.Library.Controls
{
    public class DropdownGrid : System.Windows.Controls.ComboBox
    {
        static DropdownGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DropdownGrid), new FrameworkPropertyMetadata(typeof(DropdownGrid)));
        }

        public static readonly DependencyProperty ShowSearchProperty =
            DependencyProperty.Register("ShowSearch", typeof(bool), typeof(DropdownGrid), new PropertyMetadata(false));

        public bool ShowSearch
        {
            get { return (bool)GetValue(ShowSearchProperty); }
            set { SetValue(ShowSearchProperty, value); }
        }
    }
}