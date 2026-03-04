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

    public class Drawer : ContentControl
    {
        static Drawer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Drawer), new FrameworkPropertyMetadata(typeof(Drawer)));
        }

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(Drawer), new PropertyMetadata(false, OnIsOpenChanged));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public static readonly DependencyProperty PlacementProperty =
            DependencyProperty.Register("Placement", typeof(DrawerPlacement), typeof(Drawer), new PropertyMetadata(DrawerPlacement.Right));

        public DrawerPlacement Placement
        {
            get { return (DrawerPlacement)GetValue(PlacementProperty); }
            set { SetValue(PlacementProperty, value); }
        }

        public static readonly DependencyProperty DrawerWidthProperty =
            DependencyProperty.Register("DrawerWidth", typeof(double), typeof(Drawer), new PropertyMetadata(300.0));

        public double DrawerWidth
        {
            get { return (double)GetValue(DrawerWidthProperty); }
            set { SetValue(DrawerWidthProperty, value); }
        }

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Drawer drawer)
            {
                drawer.UpdateState((bool)e.NewValue);
            }
        }

        private void UpdateState(bool isOpen)
        {
            // Animation logic will be handled by VisualState in XAML
            VisualStateManager.GoToState(this, isOpen ? "Opened" : "Closed", true);
        }
    }
}