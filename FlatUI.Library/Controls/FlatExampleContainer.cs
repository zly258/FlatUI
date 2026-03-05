using System.Windows;
using System.Windows.Controls;

namespace FlatUI.Library.Controls
{
    /// <summary>
    /// 示例容器控件 - 用于包装组件示例并添加查看源码功能
    /// </summary>
    public class FlatExampleContainer : ContentControl
    {
        static FlatExampleContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatExampleContainer), new FrameworkPropertyMetadata(typeof(FlatExampleContainer)));
        }

        #region 依赖属性

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(FlatExampleContainer), 
                new PropertyMetadata("示例"));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty XamlSourceProperty =
            DependencyProperty.Register("XamlSource", typeof(string), typeof(FlatExampleContainer), 
                new PropertyMetadata(string.Empty));

        public string XamlSource
        {
            get => (string)GetValue(XamlSourceProperty);
            set => SetValue(XamlSourceProperty, value);
        }

        public static readonly DependencyProperty CSharpSourceProperty =
            DependencyProperty.Register("CSharpSource", typeof(string), typeof(FlatExampleContainer), 
                new PropertyMetadata(string.Empty));

        public string CSharpSource
        {
            get => (string)GetValue(CSharpSourceProperty);
            set => SetValue(CSharpSourceProperty, value);
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(FlatExampleContainer), 
                new PropertyMetadata(string.Empty));

        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        #endregion

        #region 事件

        public static readonly RoutedEvent ViewSourceEvent = 
            EventManager.RegisterRoutedEvent("ViewSource", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FlatExampleContainer));

        public event RoutedEventHandler ViewSource
        {
            add => AddHandler(ViewSourceEvent, value);
            remove => RemoveHandler(ViewSourceEvent, value);
        }

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            var viewSourceButton = GetTemplateChild("PART_ViewSourceButton") as System.Windows.Controls.Button;
            if (viewSourceButton != null)
            {
                viewSourceButton.Click += OnViewSourceButtonClick;
            }
        }

        private void OnViewSourceButtonClick(object sender, RoutedEventArgs e)
        {
            var args = new RoutedEventArgs(ViewSourceEvent, this);
            RaiseEvent(args);
        }
    }
}