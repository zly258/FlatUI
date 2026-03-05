using System.Windows;
using System.Windows.Controls;

namespace FlatUI.Library.Controls
{
    /// <summary>
    /// 徽标控件 - 支持数字徽标和状态点
    /// </summary>
    public class FlatBadge : ContentControl
    {
        static FlatBadge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatBadge), new FrameworkPropertyMetadata(typeof(FlatBadge)));
        }

        #region 属性

        /// <summary>
        /// 徽标数值
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(FlatBadge), new PropertyMetadata(0));

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        /// <summary>
        /// 最大值（超过显示 99+）
        /// </summary>
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(int), typeof(FlatBadge), new PropertyMetadata(99));

        public int MaxValue
        {
            get => (int)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        /// <summary>
        /// 是否只显示状态点（不显示数字）
        /// </summary>
        public static readonly DependencyProperty IsDotProperty =
            DependencyProperty.Register("IsDot", typeof(bool), typeof(FlatBadge), new PropertyMetadata(false));

        public bool IsDot
        {
            get => (bool)GetValue(IsDotProperty);
            set => SetValue(IsDotProperty, value);
        }

        /// <summary>
        /// 是否隐藏徽标（值为 0 时）
        /// </summary>
        public static readonly DependencyProperty HiddenWhenZeroProperty =
            DependencyProperty.Register("HiddenWhenZero", typeof(bool), typeof(FlatBadge), new PropertyMetadata(false));

        public bool HiddenWhenZero
        {
            get => (bool)GetValue(HiddenWhenZeroProperty);
            set => SetValue(HiddenWhenZeroProperty, value);
        }

        /// <summary>
        /// 徽标类型（计数/状态）
        /// </summary>
        public static readonly DependencyProperty BadgeTypeProperty =
            DependencyProperty.Register("BadgeType", typeof(BadgeType), typeof(FlatBadge), new PropertyMetadata(BadgeType.Count));

        public BadgeType BadgeType
        {
            get => (BadgeType)GetValue(BadgeTypeProperty);
            set => SetValue(BadgeTypeProperty, value);
        }

        /// <summary>
        /// 状态类型（成功/警告/错误等）
        /// </summary>
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(BadgeStatus), typeof(FlatBadge), new PropertyMetadata(BadgeStatus.Default));

        public BadgeStatus Status
        {
            get => (BadgeStatus)GetValue(StatusProperty);
            set => SetValue(StatusProperty, value);
        }

        /// <summary>
        /// 徽标大小
        /// </summary>
        public static readonly DependencyProperty BadgeSizeProperty =
            DependencyProperty.Register("BadgeSize", typeof(BadgeSize), typeof(FlatBadge), new PropertyMetadata(BadgeSize.Medium));

        public BadgeSize BadgeSize
        {
            get => (BadgeSize)GetValue(BadgeSizeProperty);
            set => SetValue(BadgeSizeProperty, value);
        }

        /// <summary>
        /// 背景画刷
        /// </summary>
        public static readonly DependencyProperty BadgeBackgroundProperty =
            DependencyProperty.Register("BadgeBackground", typeof(System.Windows.Media.Brush), typeof(FlatBadge), new PropertyMetadata(null));

        public System.Windows.Media.Brush BadgeBackground
        {
            get => (System.Windows.Media.Brush)GetValue(BadgeBackgroundProperty);
            set => SetValue(BadgeBackgroundProperty, value);
        }

        /// <summary>
        /// 前景色
        /// </summary>
        public static readonly DependencyProperty BadgeForegroundProperty =
            DependencyProperty.Register("BadgeForeground", typeof(System.Windows.Media.Brush), typeof(FlatBadge), new PropertyMetadata(null));

        public System.Windows.Media.Brush BadgeForeground
        {
            get => (System.Windows.Media.Brush)GetValue(BadgeForegroundProperty);
            set => SetValue(BadgeForegroundProperty, value);
        }

        /// <summary>
        /// 相对于内容的位置偏移 X
        /// </summary>
        public static readonly DependencyProperty OffsetXProperty =
            DependencyProperty.Register("OffsetX", typeof(double), typeof(FlatBadge), new PropertyMetadata(0.0));

        public double OffsetX
        {
            get => (double)GetValue(OffsetXProperty);
            set => SetValue(OffsetXProperty, value);
        }

        /// <summary>
        /// 相对于内容的位置偏移 Y
        /// </summary>
        public static readonly DependencyProperty OffsetYProperty =
            DependencyProperty.Register("OffsetY", typeof(double), typeof(FlatBadge), new PropertyMetadata(0.0));

        public double OffsetY
        {
            get => (double)GetValue(OffsetYProperty);
            set => SetValue(OffsetYProperty, value);
        }

        /// <summary>
        /// 显示的文本（自定义）
        /// </summary>
        public static readonly DependencyProperty DisplayTextProperty =
            DependencyProperty.Register("DisplayText", typeof(string), typeof(FlatBadge), new PropertyMetadata(string.Empty));

        public string DisplayText
        {
            get => (string)GetValue(DisplayTextProperty);
            set => SetValue(DisplayTextProperty, value);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取实际显示的文本
        /// </summary>
        public string GetDisplayText()
        {
            if (!string.IsNullOrEmpty(DisplayText))
                return DisplayText;

            if (IsDot)
                return string.Empty;

            if (Value > MaxValue)
                return $"{MaxValue}+";

            return Value.ToString();
        }

        /// <summary>
        /// 判断是否应该隐藏徽标
        /// </summary>
        public bool ShouldHide()
        {
            return HiddenWhenZero && Value == 0;
        }

        #endregion
    }

    /// <summary>
    /// 徽标类型枚举
    /// </summary>
    public enum BadgeType
    {
        Count,      // 计数
        Status      // 状态
    }

    /// <summary>
    /// 徽标状态枚举
    /// </summary>
    public enum BadgeStatus
    {
        Default,    // 默认
        Success,    // 成功
        Warning,    // 警告
        Error,      // 错误
        Info,       // 信息
        Processing  // 处理中
    }

    /// <summary>
    /// 徽标尺寸枚举
    /// </summary>
    public enum BadgeSize
    {
        Small,      // 小
        Medium,     // 中
        Large       // 大
    }
}
