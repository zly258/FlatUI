using System.Windows;
using System.Windows.Controls;
using TabControl = System.Windows.Controls.TabControl;

namespace FlatUI.Library.Controls
{
    /// <summary>
    /// 扁平化Tab控件
    /// </summary>
    public class FlatTabControl : TabControl
    {
        static FlatTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatTabControl), new FrameworkPropertyMetadata(typeof(FlatTabControl)));
        }

        #region 依赖属性

        public static readonly DependencyProperty TabPositionProperty =
            DependencyProperty.Register("TabPosition", typeof(TabPosition), typeof(FlatTabControl), 
                new PropertyMetadata(TabPosition.Top));

        public TabPosition TabPosition
        {
            get => (TabPosition)GetValue(TabPositionProperty);
            set => SetValue(TabPositionProperty, value);
        }

        public static readonly DependencyProperty TabSizeProperty =
            DependencyProperty.Register("TabSize", typeof(TabSize), typeof(FlatTabControl), 
                new PropertyMetadata(TabSize.Medium));

        public TabSize TabSize
        {
            get => (TabSize)GetValue(TabSizeProperty);
            set => SetValue(TabSizeProperty, value);
        }

        public static readonly DependencyProperty ShowCloseButtonProperty =
            DependencyProperty.Register("ShowCloseButton", typeof(bool), typeof(FlatTabControl), 
                new PropertyMetadata(false));

        public bool ShowCloseButton
        {
            get => (bool)GetValue(ShowCloseButtonProperty);
            set => SetValue(ShowCloseButtonProperty, value);
        }

        #endregion
    }

    /// <summary>
    /// 标签位置枚举
    /// </summary>
    public enum TabPosition
    {
        Top,
        Bottom,
        Left,
        Right
    }

    /// <summary>
    /// 标签尺寸枚举
    /// </summary>
    public enum TabSize
    {
        Small,
        Medium,
        Large
    }
}