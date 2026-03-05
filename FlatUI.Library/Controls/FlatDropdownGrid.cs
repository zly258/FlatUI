using System.Windows;
using System.Windows.Controls;

namespace FlatUI.Library.Controls
{
    /// <summary>
    /// 下拉网格控件
    /// </summary>
    public class FlatDropdownGrid : System.Windows.Controls.ComboBox
    {
        static FlatDropdownGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatDropdownGrid), new FrameworkPropertyMetadata(typeof(FlatDropdownGrid)));
        }

        public static readonly DependencyProperty ShowSearchProperty =
            DependencyProperty.Register("ShowSearch", typeof(bool), typeof(FlatDropdownGrid), new PropertyMetadata(false));

        public bool ShowSearch
        {
            get { return (bool)GetValue(ShowSearchProperty); }
            set { SetValue(ShowSearchProperty, value); }
        }
    }
}