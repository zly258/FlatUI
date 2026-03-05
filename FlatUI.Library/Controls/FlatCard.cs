using System.Windows;
using System.Windows.Controls;

namespace FlatUI.Library.Controls
{
    /// <summary>
    /// 卡片控件
    /// </summary>
    public class FlatCard : ContentControl
    {
        static FlatCard()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatCard), new FrameworkPropertyMetadata(typeof(FlatCard)));
        }

        #region 依赖属性

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(object), typeof(FlatCard), new PropertyMetadata(null));

        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public static readonly DependencyProperty ShowHeaderProperty =
            DependencyProperty.Register("ShowHeader", typeof(bool), typeof(FlatCard), new PropertyMetadata(true));

        public bool ShowHeader
        {
            get => (bool)GetValue(ShowHeaderProperty);
            set => SetValue(ShowHeaderProperty, value);
        }

        public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(FlatCard), new PropertyMetadata(null));

        public DataTemplate HeaderTemplate
        {
            get => (DataTemplate)GetValue(HeaderTemplateProperty);
            set => SetValue(HeaderTemplateProperty, value);
        }

        public static readonly DependencyProperty FooterProperty = DependencyProperty.Register("Footer", typeof(object), typeof(FlatCard), new PropertyMetadata(null));

        public object Footer
        {
            get => GetValue(FooterProperty);
            set => SetValue(FooterProperty, value);
        }

        public static readonly DependencyProperty ShowFooterProperty =
            DependencyProperty.Register("ShowFooter", typeof(bool), typeof(FlatCard), new PropertyMetadata(false));

        public bool ShowFooter
        {
            get => (bool)GetValue(ShowFooterProperty);
            set => SetValue(ShowFooterProperty, value);
        }

        public static readonly DependencyProperty FooterTemplateProperty = DependencyProperty.Register("FooterTemplate", typeof(DataTemplate), typeof(FlatCard), new PropertyMetadata(null));

        public DataTemplate FooterTemplate
        {
            get => (DataTemplate)GetValue(FooterTemplateProperty);
            set => SetValue(FooterTemplateProperty, value);
        }

        public static readonly DependencyProperty ShadowDepthProperty =
            DependencyProperty.Register("ShadowDepth", typeof(ShadowDepth), typeof(FlatCard), new PropertyMetadata(ShadowDepth.Medium));

        public ShadowDepth ShadowDepth
        {
            get => (ShadowDepth)GetValue(ShadowDepthProperty);
            set => SetValue(ShadowDepthProperty, value);
        }

        public static readonly DependencyProperty IsHoverableProperty =
            DependencyProperty.Register("IsHoverable", typeof(bool), typeof(FlatCard), new PropertyMetadata(false));

        public bool IsHoverable
        {
            get => (bool)GetValue(IsHoverableProperty);
            set => SetValue(IsHoverableProperty, value);
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(FlatCard), new PropertyMetadata(new CornerRadius(4)));

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        #endregion
    }

    /// <summary>
    /// 阴影深度枚举
    /// </summary>
    public enum ShadowDepth
    {
        None,
        Small,
        Medium,
        Large
    }
}