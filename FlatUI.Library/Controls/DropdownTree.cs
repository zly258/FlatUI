using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FlatUI.Library.Controls
{
    public class DropdownTree : ComboBox
    {
        static DropdownTree()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DropdownTree), new FrameworkPropertyMetadata(typeof(DropdownTree)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}