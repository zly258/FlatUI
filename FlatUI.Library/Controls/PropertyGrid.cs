using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace FlatUI.Library.Controls
{
    public class PropertyItem
    {
        public string Category { get; set; } = "General";
        public string Name { get; set; } = string.Empty;
        public object? Value { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class PropertyGrid : System.Windows.Controls.Control
    {
        static PropertyGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGrid), new FrameworkPropertyMetadata(typeof(PropertyGrid)));
        }

        public static readonly DependencyProperty PropertiesProperty =
            DependencyProperty.Register("Properties", typeof(ObservableCollection<PropertyItem>), typeof(PropertyGrid), 
                new PropertyMetadata(null, OnPropertiesChanged));

        public ObservableCollection<PropertyItem> Properties
        {
            get { return (ObservableCollection<PropertyItem>)GetValue(PropertiesProperty); }
            set { SetValue(PropertiesProperty, value); }
        }

        public static readonly DependencyProperty InternalCollectionViewProperty =
            DependencyProperty.Register("InternalCollectionView", typeof(ICollectionView), typeof(PropertyGrid), new PropertyMetadata(null));

        public ICollectionView InternalCollectionView
        {
            get { return (ICollectionView)GetValue(InternalCollectionViewProperty); }
            set { SetValue(InternalCollectionViewProperty, value); }
        }

        private static void OnPropertiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PropertyGrid pg && e.NewValue is ObservableCollection<PropertyItem> items)
            {
                var view = CollectionViewSource.GetDefaultView(items);
                view.GroupDescriptions.Clear();
                view.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
                pg.InternalCollectionView = view;
            }
        }
    }
}