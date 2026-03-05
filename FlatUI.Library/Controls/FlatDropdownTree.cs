using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FlatUI.Library.Controls
{
    /// <summary>
    /// 下拉树控件
    /// </summary>
    public class FlatDropdownTree : System.Windows.Controls.ComboBox
    {
        static FlatDropdownTree()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatDropdownTree), new FrameworkPropertyMetadata(typeof(FlatDropdownTree)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}