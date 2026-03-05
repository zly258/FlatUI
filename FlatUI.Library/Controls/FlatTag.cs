using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FlatUI.Library.Controls
{
    /// <summary>
    /// 标签控件 - 支持可关闭、可编辑、可选择
    /// </summary>
    public class FlatTag : ContentControl
    {
        static FlatTag()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatTag), new FrameworkPropertyMetadata(typeof(FlatTag)));
        }

        #region 事件

        /// <summary>
        /// 关闭按钮点击事件
        /// </summary>
        public event RoutedEventHandler CloseClick
        {
            add => AddHandler(CloseClickEvent, value);
            remove => RemoveHandler(CloseClickEvent, value);
        }

        public static readonly RoutedEvent CloseClickEvent =
            EventManager.RegisterRoutedEvent("CloseClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FlatTag));

        /// <summary>
        /// 编辑完成事件
        /// </summary>
        public event RoutedEventHandler EditComplete
        {
            add => AddHandler(EditCompleteEvent, value);
            remove => RemoveHandler(EditCompleteEvent, value);
        }

        public static readonly RoutedEvent EditCompleteEvent =
            EventManager.RegisterRoutedEvent("EditComplete", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FlatTag));

        #endregion

        #region 属性

        /// <summary>
        /// 是否显示关闭按钮
        /// </summary>
        public static readonly DependencyProperty IsClosableProperty = DependencyProperty.Register("IsClosable", typeof(bool), typeof(FlatTag), new PropertyMetadata(false));

        public bool IsClosable
        {
            get => (bool)GetValue(IsClosableProperty);
            set => SetValue(IsClosableProperty, value);
        }

        /// <summary>
        /// 是否可编辑
        /// </summary>
        public static readonly DependencyProperty IsEditableProperty = DependencyProperty.Register("IsEditable", typeof(bool), typeof(FlatTag), new PropertyMetadata(false));

        public bool IsEditable
        {
            get => (bool)GetValue(IsEditableProperty);
            set => SetValue(IsEditableProperty, value);
        }

        /// <summary>
        /// 是否可选中
        /// </summary>
        public static readonly DependencyProperty IsSelectableProperty = DependencyProperty.Register("IsSelectable", typeof(bool), typeof(FlatTag), new PropertyMetadata(false));

        public bool IsSelectable
        {
            get => (bool)GetValue(IsSelectableProperty);
            set => SetValue(IsSelectableProperty, value);
        }

        /// <summary>
        /// 是否被选中
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(FlatTag), new PropertyMetadata(false));

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        /// <summary>
        /// 是否正在编辑
        /// </summary>
        public static readonly DependencyProperty IsEditingProperty =
            DependencyProperty.Register("IsEditing", typeof(bool), typeof(FlatTag), new PropertyMetadata(false));

        public bool IsEditing
        {
            get => (bool)GetValue(IsEditingProperty);
            set => SetValue(IsEditingProperty, value);
        }

        /// <summary>
        /// 标签类型（样式）
        /// </summary>
        public static readonly DependencyProperty TagTypeProperty =
            DependencyProperty.Register("TagType", typeof(TagType), typeof(FlatTag), new PropertyMetadata(TagType.Default));

        public TagType TagType
        {
            get => (TagType)GetValue(TagTypeProperty);
            set => SetValue(TagTypeProperty, value);
        }

        /// <summary>
        /// 图标
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(Geometry), typeof(FlatTag), new PropertyMetadata(null));

        public Geometry Icon
        {
            get => (Geometry)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        /// <summary>
        /// 背景画刷
        /// </summary>
        public static readonly DependencyProperty TagBackgroundProperty =
            DependencyProperty.Register("TagBackground", typeof(System.Windows.Media.Brush), typeof(FlatTag), new PropertyMetadata(null));

        public System.Windows.Media.Brush TagBackground
        {
            get => (System.Windows.Media.Brush)GetValue(TagBackgroundProperty);
            set => SetValue(TagBackgroundProperty, value);
        }

        /// <summary>
        /// 前景色
        /// </summary>
        public static readonly DependencyProperty TagForegroundProperty =
            DependencyProperty.Register("TagForeground", typeof(System.Windows.Media.Brush), typeof(FlatTag), new PropertyMetadata(null));

        public System.Windows.Media.Brush TagForeground
        {
            get => (System.Windows.Media.Brush)GetValue(TagForegroundProperty);
            set => SetValue(TagForegroundProperty, value);
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        public static readonly DependencyProperty BorderColorProperty =
            DependencyProperty.Register("BorderColor", typeof(System.Windows.Media.Brush), typeof(FlatTag), new PropertyMetadata(null));

        public System.Windows.Media.Brush BorderColor
        {
            get => (System.Windows.Media.Brush)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        /// <summary>
        /// 标签大小
        /// </summary>
        public static readonly DependencyProperty TagSizeProperty =
            DependencyProperty.Register("TagSize", typeof(TagSize), typeof(FlatTag), new PropertyMetadata(TagSize.Medium));

        public TagSize TagSize
        {
            get => (TagSize)GetValue(TagSizeProperty);
            set => SetValue(TagSizeProperty, value);
        }

        /// <summary>
        /// 圆角半径
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(FlatTag), new PropertyMetadata(new CornerRadius(4)));

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        #endregion

        #region 方法

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            
            if (IsSelectable)
            {
                IsSelected = !IsSelected;
            }

            if (IsEditable && !IsEditing)
            {
                StartEditing();
            }
        }

        /// <summary>
        /// 触发关闭事件
        /// </summary>
        public void OnCloseClick()
        {
            RaiseEvent(new RoutedEventArgs(CloseClickEvent, this));
        }

        /// <summary>
        /// 开始编辑
        /// </summary>
        public void StartEditing()
        {
            IsEditing = true;
        }

        /// <summary>
        /// 结束编辑
        /// </summary>
        public void EndEditing()
        {
            IsEditing = false;
            RaiseEvent(new RoutedEventArgs(EditCompleteEvent, this));
        }

        #endregion
    }

    /// <summary>
    /// 标签类型枚举
    /// </summary>
    public enum TagType
    {
        Default,    // 默认
        Primary,    // 主要
        Success,    // 成功
        Warning,    // 警告
        Error,      // 错误
        Info        // 信息
    }

    /// <summary>
    /// 标签尺寸枚举
    /// </summary>
    public enum TagSize
    {
        Small,      // 小
        Medium,     // 中
        Large       // 大
    }
}
