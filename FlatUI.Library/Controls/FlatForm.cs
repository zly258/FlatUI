using System.Windows;
using System.Windows.Controls;

namespace FlatUI.Library.Controls
{
    public class FormItem : HeaderedContentControl
    {
        static FormItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FormItem), new FrameworkPropertyMetadata(typeof(FormItem)));
        }
    }

    /// <summary>
    /// 表单控件
    /// </summary>
    public class FlatForm : ItemsControl
    {
        static FlatForm()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatForm), new FrameworkPropertyMetadata(typeof(FlatForm)));
        }
    }
}