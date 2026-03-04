using System.Windows;
using System.Windows.Controls;

namespace FlatUI.Library.Controls
{
    public class DropdownGrid : ComboBox
    {
        static DropdownGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DropdownGrid), new FrameworkPropertyMetadata(typeof(DropdownGrid)));
        }
    }
}