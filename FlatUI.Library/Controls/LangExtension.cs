using System;
using System.Windows.Markup;

namespace FlatUI.Library.Controls
{
    /// <summary>
    /// XAML 标记扩展 - 用于资源字符串
    /// </summary>
    public class LangExtension : MarkupExtension
    {
        public string? Key { get; set; }

        public LangExtension()
        {
        }

        public LangExtension(string key)
        {
            Key = key;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(Key))
                return Key ?? string.Empty;

            return LanguageManager.Instance.GetString(Key);
        }
    }
}
