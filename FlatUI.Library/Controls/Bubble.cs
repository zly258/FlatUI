using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FlatUI.Library.Controls
{
    /// <summary>
    /// 气泡卡片控件 - 用于对话、评论等场景
    /// </summary>
    public class Bubble : ContentControl
    {
        static Bubble()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Bubble), new FrameworkPropertyMetadata(typeof(Bubble)));
        }

        #region 属性

        /// <summary>
        /// 头像图片源
        /// </summary>
        public static readonly DependencyProperty AvatarSourceProperty =
            DependencyProperty.Register("AvatarSource", typeof(ImageSource), typeof(Bubble), new PropertyMetadata(null));

        public ImageSource AvatarSource
        {
            get => (ImageSource)GetValue(AvatarSourceProperty);
            set => SetValue(AvatarSourceProperty, value);
        }

        /// <summary>
        /// 头像文字（无图片时显示）
        /// </summary>
        public static readonly DependencyProperty AvatarTextProperty =
            DependencyProperty.Register("AvatarText", typeof(string), typeof(Bubble), new PropertyMetadata(string.Empty));

        public string AvatarText
        {
            get => (string)GetValue(AvatarTextProperty);
            set => SetValue(AvatarTextProperty, value);
        }

        /// <summary>
        /// 发送者名称
        /// </summary>
        public static readonly DependencyProperty SenderNameProperty =
            DependencyProperty.Register("SenderName", typeof(string), typeof(Bubble), new PropertyMetadata(string.Empty));

        public string SenderName
        {
            get => (string)GetValue(SenderNameProperty);
            set => SetValue(SenderNameProperty, value);
        }

        /// <summary>
        /// 时间戳
        /// </summary>
        public static readonly DependencyProperty TimestampProperty =
            DependencyProperty.Register("Timestamp", typeof(DateTime), typeof(Bubble), new PropertyMetadata(DateTime.Now));

        public DateTime Timestamp
        {
            get => (DateTime)GetValue(TimestampProperty);
            set => SetValue(TimestampProperty, value);
        }

        /// <summary>
        /// 是否显示时间戳
        /// </summary>
        public static readonly DependencyProperty ShowTimestampProperty =
            DependencyProperty.Register("ShowTimestamp", typeof(bool), typeof(Bubble), new PropertyMetadata(true));

        public bool ShowTimestamp
        {
            get => (bool)GetValue(ShowTimestampProperty);
            set => SetValue(ShowTimestampProperty, value);
        }

        /// <summary>
        /// 是否显示发送者名称
        /// </summary>
        public static readonly DependencyProperty ShowSenderNameProperty =
            DependencyProperty.Register("ShowSenderName", typeof(bool), typeof(Bubble), new PropertyMetadata(true));

        public bool ShowSenderName
        {
            get => (bool)GetValue(ShowSenderNameProperty);
            set => SetValue(ShowSenderNameProperty, value);
        }

        /// <summary>
        /// 是否显示头像
        /// </summary>
        public static readonly DependencyProperty ShowAvatarProperty =
            DependencyProperty.Register("ShowAvatar", typeof(bool), typeof(Bubble), new PropertyMetadata(true));

        public bool ShowAvatar
        {
            get => (bool)GetValue(ShowAvatarProperty);
            set => SetValue(ShowAvatarProperty, value);
        }

        /// <summary>
        /// 气泡位置（左/右）
        /// </summary>
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(BubblePosition), typeof(Bubble), new PropertyMetadata(BubblePosition.Left));

        public BubblePosition Position
        {
            get => (BubblePosition)GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }

        /// <summary>
        /// 气泡类型
        /// </summary>
        public static readonly DependencyProperty BubbleTypeProperty =
            DependencyProperty.Register("BubbleType", typeof(BubbleType), typeof(Bubble), new PropertyMetadata(BubbleType.Default));

        public BubbleType BubbleType
        {
            get => (BubbleType)GetValue(BubbleTypeProperty);
            set => SetValue(BubbleTypeProperty, value);
        }

        /// <summary>
        /// 背景画刷
        /// </summary>
        public static readonly DependencyProperty BubbleBackgroundProperty =
            DependencyProperty.Register("BubbleBackground", typeof(System.Windows.Media.Brush), typeof(Bubble), new PropertyMetadata(null));

        public System.Windows.Media.Brush BubbleBackground
        {
            get => (System.Windows.Media.Brush)GetValue(BubbleBackgroundProperty);
            set => SetValue(BubbleBackgroundProperty, value);
        }

        /// <summary>
        /// 前景色
        /// </summary>
        public static readonly DependencyProperty BubbleForegroundProperty =
            DependencyProperty.Register("BubbleForeground", typeof(System.Windows.Media.Brush), typeof(Bubble), new PropertyMetadata(null));

        public System.Windows.Media.Brush BubbleForeground
        {
            get => (System.Windows.Media.Brush)GetValue(BubbleForegroundProperty);
            set => SetValue(BubbleForegroundProperty, value);
        }

        /// <summary>
        /// 最大宽度
        /// </summary>
        public new static readonly DependencyProperty MaxWidthProperty =
            DependencyProperty.Register("MaxWidth", typeof(double), typeof(Bubble), new PropertyMetadata(400.0));

        public new double MaxWidth
        {
            get => (double)GetValue(MaxWidthProperty);
            set => SetValue(MaxWidthProperty, value);
        }

        /// <summary>
        /// 圆角半径
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(Bubble), new PropertyMetadata(new CornerRadius(8)));

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        /// 头像大小
        /// </summary>
        public static readonly DependencyProperty AvatarSizeProperty =
            DependencyProperty.Register("AvatarSize", typeof(double), typeof(Bubble), new PropertyMetadata(40.0));

        public double AvatarSize
        {
            get => (double)GetValue(AvatarSizeProperty);
            set => SetValue(AvatarSizeProperty, value);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取格式化的时间字符串
        /// </summary>
        public string GetFormattedTime()
        {
            var now = DateTime.Now;
            var diff = now - Timestamp;

            if (diff.TotalMinutes < 1)
                return "刚刚";
            else if (diff.TotalMinutes < 60)
                return $"{(int)diff.TotalMinutes}分钟前";
            else if (diff.TotalHours < 24)
                return $"{(int)diff.TotalHours}小时前";
            else if (diff.TotalDays < 7)
                return $"{(int)diff.TotalDays}天前";
            else
                return Timestamp.ToString("yyyy-MM-dd HH:mm");
        }

        #endregion
    }

    /// <summary>
    /// 气泡位置枚举
    /// </summary>
    public enum BubblePosition
    {
        Left,   // 左侧
        Right   // 右侧
    }

    /// <summary>
    /// 气泡类型枚举
    /// </summary>
    public enum BubbleType
    {
        Default,    // 默认
        Primary,    // 主要
        Success,    // 成功
        Warning,    // 警告
        Error       // 错误
    }
}
