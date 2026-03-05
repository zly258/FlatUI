using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace FlatUI.Library.Controls
{
    /// <summary>
    /// 开关组件 - 用于表示开/关状态，与Checkbox区分
    /// </summary>
    public class FlatSwitch : ToggleButton
    {
        static FlatSwitch()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatSwitch), new FrameworkPropertyMetadata(typeof(FlatSwitch)));
        }

        #region 依赖属性

        public static readonly DependencyProperty OnContentProperty = DependencyProperty.Register("OnContent", typeof(object), typeof(FlatSwitch), new PropertyMetadata("开"));

        public object OnContent
        {
            get => GetValue(OnContentProperty);
            set => SetValue(OnContentProperty, value);
        }

        public static readonly DependencyProperty OffContentProperty = DependencyProperty.Register("OffContent", typeof(object), typeof(FlatSwitch), new PropertyMetadata("关"));

        public object OffContent
        {
            get => GetValue(OffContentProperty);
            set => SetValue(OffContentProperty, value);
        }

        public static readonly DependencyProperty ShowTextProperty =
            DependencyProperty.Register("ShowText", typeof(bool), typeof(FlatSwitch), new PropertyMetadata(true));

        public bool ShowText
        {
            get => (bool)GetValue(ShowTextProperty);
            set => SetValue(ShowTextProperty, value);
        }

        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register("Size", typeof(SwitchSize), typeof(FlatSwitch), new PropertyMetadata(SwitchSize.Medium));

        public SwitchSize Size
        {
            get => (SwitchSize)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        #endregion
    }

    /// <summary>
    /// 开关尺寸枚举
    /// </summary>
    public enum SwitchSize
    {
        Small,
        Medium,
        Large
    }
}