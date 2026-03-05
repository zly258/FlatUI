using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FlatUI.Library.Controls
{
    /// <summary>
    /// 头像控件 - 支持图片、文字、图标三种模式
    /// </summary>
    public class Avatar : System.Windows.Controls.Control
    {
        static Avatar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Avatar), new FrameworkPropertyMetadata(typeof(Avatar)));
        }

        #region 属性

        /// <summary>
        /// 头像图片源
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(Avatar), new PropertyMetadata(null));

        public ImageSource Source
        {
            get => (ImageSource)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        /// <summary>
        /// 显示的文字（当没有图片时显示）
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(Avatar), new PropertyMetadata(string.Empty));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// 显示的图标
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(Geometry), typeof(Avatar), new PropertyMetadata(null));

        public Geometry Icon
        {
            get => (Geometry)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        /// <summary>
        /// 头像尺寸（直径）
        /// </summary>
        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register("Size", typeof(double), typeof(Avatar), new PropertyMetadata(40.0));

        public double Size
        {
            get => (double)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        /// <summary>
        /// 形状类型（圆形/圆角矩形）
        /// </summary>
        public static readonly DependencyProperty ShapeProperty =
            DependencyProperty.Register("Shape", typeof(AvatarShape), typeof(Avatar), new PropertyMetadata(AvatarShape.Circle));

        public AvatarShape Shape
        {
            get => (AvatarShape)GetValue(ShapeProperty);
            set => SetValue(ShapeProperty, value);
        }

        /// <summary>
        /// 背景画刷
        /// </summary>
        public static readonly DependencyProperty AvatarBackgroundProperty =
            DependencyProperty.Register("AvatarBackground", typeof(System.Windows.Media.Brush), typeof(Avatar), new PropertyMetadata(null));

        public System.Windows.Media.Brush AvatarBackground
        {
            get => (System.Windows.Media.Brush)GetValue(AvatarBackgroundProperty);
            set => SetValue(AvatarBackgroundProperty, value);
        }

        /// <summary>
        /// 前景色/文字颜色
        /// </summary>
        public static readonly DependencyProperty AvatarForegroundProperty =
            DependencyProperty.Register("AvatarForeground", typeof(System.Windows.Media.Brush), typeof(Avatar), new PropertyMetadata(null));

        public System.Windows.Media.Brush AvatarForeground
        {
            get => (System.Windows.Media.Brush)GetValue(AvatarForegroundProperty);
            set => SetValue(AvatarForegroundProperty, value);
        }

        /// <summary>
        /// 字体大小
        /// </summary>
        public static readonly DependencyProperty FontSizeProperty =
            DependencyProperty.Register("FontSize", typeof(double), typeof(Avatar), new PropertyMetadata(16.0));

        public new double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        /// <summary>
        /// 是否启用边框
        /// </summary>
        public static readonly DependencyProperty ShowBorderProperty =
            DependencyProperty.Register("ShowBorder", typeof(bool), typeof(Avatar), new PropertyMetadata(false));

        public bool ShowBorder
        {
            get => (bool)GetValue(ShowBorderProperty);
            set => SetValue(ShowBorderProperty, value);
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        public static readonly DependencyProperty BorderColorProperty =
            DependencyProperty.Register("BorderColor", typeof(System.Windows.Media.Brush), typeof(Avatar), new PropertyMetadata(null));

        public System.Windows.Media.Brush BorderColor
        {
            get => (System.Windows.Media.Brush)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        /// <summary>
        /// 边框厚度
        /// </summary>
        public new static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register("BorderThickness", typeof(double), typeof(Avatar), new PropertyMetadata(2.0));

        public new double BorderThickness
        {
            get => (double)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取显示模式（优先级：图片 > 图标 > 文字）
        /// </summary>
        public DisplayMode GetDisplayMode()
        {
            if (Source != null) return DisplayMode.Image;
            if (Icon != null) return DisplayMode.Icon;
            return DisplayMode.Text;
        }

        #endregion
    }

    /// <summary>
    /// 头像形状枚举
    /// </summary>
    public enum AvatarShape
    {
        Circle,         // 圆形
        RoundedSquare   // 圆角矩形
    }

    /// <summary>
    /// 显示模式枚举
    /// </summary>
    public enum DisplayMode
    {
        Image,  // 图片
        Icon,   // 图标
        Text    // 文字
    }
}
