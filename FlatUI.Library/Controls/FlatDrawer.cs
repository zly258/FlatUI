using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace FlatUI.Library.Controls
{
    public enum DrawerPlacement
    {
        Left,
        Right,
        Top,
        Bottom
    }

    public class FlatDrawer : ContentControl
    {
        static FlatDrawer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatDrawer), new FrameworkPropertyMetadata(typeof(FlatDrawer)));
        }

        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(FlatDrawer), new PropertyMetadata(false, OnIsOpenChanged));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public static readonly DependencyProperty PlacementProperty =
            DependencyProperty.Register("Placement", typeof(DrawerPlacement), typeof(FlatDrawer), new PropertyMetadata(DrawerPlacement.Left));

        public DrawerPlacement Placement
        {
            get { return (DrawerPlacement)GetValue(PlacementProperty); }
            set { SetValue(PlacementProperty, value); }
        }

        public static readonly DependencyProperty DrawerWidthProperty =
            DependencyProperty.Register("DrawerWidth", typeof(double), typeof(FlatDrawer), new PropertyMetadata(300.0));

        public double DrawerWidth
        {
            get { return (double)GetValue(DrawerWidthProperty); }
            set { SetValue(DrawerWidthProperty, value); }
        }

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FlatDrawer drawer)
            {
                drawer.UpdateState((bool)e.NewValue);
            }
        }

        private void UpdateState(bool isOpen)
        {
            VisualStateManager.GoToState(this, isOpen ? "Opened" : "Closed", true);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            VisualStateManager.GoToState(this, IsOpen ? "Opened" : "Closed", false);
        }
    }
}