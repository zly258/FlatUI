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

    /// <summary>
    /// 属性网格控件
    /// </summary>
    public class FlatPropertyGrid : System.Windows.Controls.Control
    {
        static FlatPropertyGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatPropertyGrid), new FrameworkPropertyMetadata(typeof(FlatPropertyGrid)));
        }

        public static readonly DependencyProperty PropertiesProperty =
            DependencyProperty.Register("Properties", typeof(ObservableCollection<PropertyItem>), typeof(FlatPropertyGrid), 
                new PropertyMetadata(null, OnPropertiesChanged));

        public ObservableCollection<PropertyItem> Properties
        {
            get { return (ObservableCollection<PropertyItem>)GetValue(PropertiesProperty); }
            set { SetValue(PropertiesProperty, value); }
        }

        public static readonly DependencyProperty InternalCollectionViewProperty =
            DependencyProperty.Register("InternalCollectionView", typeof(ICollectionView), typeof(FlatPropertyGrid), new PropertyMetadata(null));

        public ICollectionView InternalCollectionView
        {
            get { return (ICollectionView)GetValue(InternalCollectionViewProperty); }
            set { SetValue(InternalCollectionViewProperty, value); }
        }

        private static void OnPropertiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FlatPropertyGrid pg && e.NewValue is ObservableCollection<PropertyItem> items)
            {
                var view = CollectionViewSource.GetDefaultView(items);
                view.GroupDescriptions.Clear();
                view.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
                pg.InternalCollectionView = view;
            }
        }
    }
}