using System;
using System.Windows;
using System.Windows.Controls;

namespace FlatUI.Library.Controls
{
    /// <summary>
    /// Toast通知控件
    /// </summary>
    public class FlatToast : ContentControl
    {
        static FlatToast()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatToast), new FrameworkPropertyMetadata(typeof(FlatToast)));
        }

        #region 依赖属性

        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(ToastType), typeof(FlatToast), new PropertyMetadata(ToastType.Info));

        public ToastType Type
        {
            get => (ToastType)GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }

        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(int), typeof(FlatToast), new PropertyMetadata(3000));

        public int Duration
        {
            get => (int)GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }

        public static readonly DependencyProperty ShowIconProperty =
            DependencyProperty.Register("ShowIcon", typeof(bool), typeof(FlatToast), new PropertyMetadata(true));

        public bool ShowIcon
        {
            get => (bool)GetValue(ShowIconProperty);
            set => SetValue(ShowIconProperty, value);
        }

        public static readonly DependencyProperty ShowCloseButtonProperty =
            DependencyProperty.Register("ShowCloseButton", typeof(bool), typeof(FlatToast), new PropertyMetadata(true));

        public bool ShowCloseButton
        {
            get => (bool)GetValue(ShowCloseButtonProperty);
            set => SetValue(ShowCloseButtonProperty, value);
        }

        #endregion

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(FlatToast), new PropertyMetadata(new CornerRadius(4)));

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        #region 事件

        public static readonly RoutedEvent ClosedEvent = 
            EventManager.RegisterRoutedEvent("Closed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FlatToast));

        public event RoutedEventHandler Closed
        {
            add => AddHandler(ClosedEvent, value);
            remove => RemoveHandler(ClosedEvent, value);
        }

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            var closeButton = GetTemplateChild("PART_CloseButton") as System.Windows.Controls.Button;
            if (closeButton != null)
            {
                closeButton.Click += OnCloseButtonClick;
            }
        }

        private void OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 关闭Toast
        /// </summary>
        public void Close()
        {
            var args = new RoutedEventArgs(ClosedEvent, this);
            RaiseEvent(args);
        }

        /// <summary>
        /// 显示Toast
        /// </summary>
        public void Show()
        {
            // Toast显示逻辑将在ToastService中实现
        }
    }

    /// <summary>
    /// Toast类型枚举
    /// </summary>
    public enum ToastType
    {
        Info,
        Success,
        Warning,
        Error
    }
}