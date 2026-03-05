using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Windows;

namespace FlatUI.Library.Controls
{
    /// <summary>
    /// 语言管理器 - 单例模式
    /// </summary>
    public class LanguageManager
    {
        private static readonly object LockObj = new object();
        private static LanguageManager _instance = null!;
        
        // 支持的语言列表
        public static readonly CultureInfo[] SupportedCultures = new[]
        {
            CultureInfo.GetCultureInfo("zh-CN"),  // 简体中文
            CultureInfo.GetCultureInfo("en-US")   // 英语
        };

        private LanguageManager()
        {
            // 默认使用中文
            CurrentCulture = CultureInfo.GetCultureInfo("zh-CN");
        }

        public static LanguageManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new LanguageManager();
                        }
                    }
                }
                return _instance;
            }
        }

        private CultureInfo _currentCulture = null!;
        
        /// <summary>
        /// 当前文化
        /// </summary>
        public CultureInfo CurrentCulture
        {
            get => _currentCulture;
            set
            {
                if (_currentCulture != value)
                {
                    _currentCulture = value;
                    Thread.CurrentThread.CurrentUICulture = value;
                    Thread.CurrentThread.CurrentCulture = value;
                    
                    // 触发语言切换事件
                    LanguageChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// 语言切换事件
        /// </summary>
        public event EventHandler? LanguageChanged;

        /// <summary>
        /// 获取资源字符串
        /// </summary>
        public string GetString(string key)
        {
            try
            {
                // 直接使用 ResourceManager，不使用 Properties.Resources
                var resourceManager = new ResourceManager("FlatUI.Library.Resources.Resources", typeof(LanguageManager).Assembly);
                return resourceManager?.GetString(key, CurrentCulture) ?? key;
            }
            catch
            {
                return key;
            }
        }

        /// <summary>
        /// 获取格式化资源字符串
        /// </summary>
        public string GetString(string key, params object?[] args)
        {
            try
            {
                var resourceManager = new ResourceManager("FlatUI.Library.Resources.Resources", typeof(LanguageManager).Assembly);
                var format = resourceManager?.GetString(key, CurrentCulture) ?? key;
                return string.Format(CurrentCulture, format, args);
            }
            catch
            {
                return key;
            }
        }

        /// <summary>
        /// 切换到中文
        /// </summary>
        public void SwitchToChinese()
        {
            CurrentCulture = CultureInfo.GetCultureInfo("zh-CN");
        }

        /// <summary>
        /// 切换到英文
        /// </summary>
        public void SwitchToEnglish()
        {
            CurrentCulture = CultureInfo.GetCultureInfo("en-US");
        }
    }

    /// <summary>
    /// 语言扩展方法 - 用于 XAML 绑定
    /// </summary>
    public static class LanguageExtension
    {
        /// <summary>
        /// 获取本地化字符串
        /// </summary>
        public static string Lang(this string key)
        {
            return LanguageManager.Instance.GetString(key);
        }

        /// <summary>
        /// 获取格式化的本地化字符串
        /// </summary>
        public static string Lang(this string key, params object?[] args)
        {
            return LanguageManager.Instance.GetString(key, args);
        }
    }
}
