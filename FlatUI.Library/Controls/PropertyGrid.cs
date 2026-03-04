using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace FlatUI.Library.Controls
{
    public class PropertyItem
    {
        public string Category { get; set; } = "General";
        public string Name { get; set; } = string.Empty;
        public object? Value { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class PropertyGrid : Control
    {
        static PropertyGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGrid), new FrameworkPropertyMetadata(typeof(PropertyGrid)));
        }

        public static readonly DependencyProperty PropertiesProperty =
            DependencyProperty.Register("Properties", typeof(ObservableCollection<PropertyItem>), typeof(PropertyGrid), new PropertyMetadata(null));

        public ObservableCollection<PropertyItem> Properties
        {
            get { return (ObservableCollection<PropertyItem>)GetValue(PropertiesProperty); }
            set { SetValue(PropertiesProperty, value); }
        }
    }
}