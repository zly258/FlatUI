using System.Windows;
using System.Windows.Controls;

namespace FlatUI.Library.Controls
{
    /// <summary>
    /// 源码查看器控件
    /// </summary>
    public class FlatSourceViewer : ContentControl
    {
        static FlatSourceViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatSourceViewer), new FrameworkPropertyMetadata(typeof(FlatSourceViewer)));
        }

        #region 依赖属性

        public static readonly DependencyProperty SourceCodeProperty = DependencyProperty.Register("SourceCode", typeof(string), typeof(FlatSourceViewer), new PropertyMetadata(string.Empty));

        public string SourceCode
        {
            get => (string)GetValue(SourceCodeProperty);
            set => SetValue(SourceCodeProperty, value);
        }

        public static readonly DependencyProperty SourceLanguageProperty = DependencyProperty.Register("SourceLanguage", typeof(string), typeof(FlatSourceViewer), new PropertyMetadata("xml"));

        public string SourceLanguage
        {
            get => (string)GetValue(SourceLanguageProperty);
            set => SetValue(SourceLanguageProperty, value);
        }

        public static readonly DependencyProperty ShowLineNumbersProperty = DependencyProperty.Register("ShowLineNumbers", typeof(bool), typeof(FlatSourceViewer), new PropertyMetadata(true));

        public bool ShowLineNumbers
        {
            get => (bool)GetValue(ShowLineNumbersProperty);
            set => SetValue(ShowLineNumbersProperty, value);
        }

        #endregion

        #region 事件

        public static readonly RoutedEvent CopyToClipboardEvent = EventManager.RegisterRoutedEvent("CopyToClipboard", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FlatSourceViewer));

        public event RoutedEventHandler CopyToClipboard
        {
            add => AddHandler(CopyToClipboardEvent, value);
            remove => RemoveHandler(CopyToClipboardEvent, value);
        }

        #endregion
    }
}