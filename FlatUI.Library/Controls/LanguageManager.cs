using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Windows;
using System.Windows.Markup;

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

        // 动态资源字典缓存
        private readonly Dictionary<string, ResourceDictionary> _resourceCache = new Dictionary<string, ResourceDictionary>();

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
        /// 切换语言并通知所有窗口
        /// </summary>
        public void ChangeLanguage(CultureInfo culture)
        {
            CurrentCulture = culture;
            NotifyLanguageChanged();
        }

        private void NotifyLanguageChanged()
        {
            LanguageChanged?.Invoke(this, EventArgs.Empty);

            foreach (Window window in System.Windows.Application.Current.Windows)
            {
                window.Language = XmlLanguage.GetLanguage(CurrentCulture.IetfLanguageTag);
            }

            // 刷新动态资源
            RefreshDynamicResources();
        }

        /// <summary>
        /// 刷新动态资源
        /// </summary>
        private void RefreshDynamicResources()
        {
            try
            {
                // 清除缓存
                _resourceCache.Clear();

                // 通知所有窗口刷新资源
                foreach (Window window in Application.Current.Windows)
                {
                    RefreshWindowResources(window);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"刷新资源失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 刷新窗口资源
        /// </summary>
        private void RefreshWindowResources(Window window)
        {
            if (window.Resources == null) return;

            // 重新加载资源字典
            var resourcesToReload = new List<ResourceDictionary>();
            
            foreach (ResourceDictionary dict in window.Resources.MergedDictionaries)
            {
                if (dict.Source?.ToString().Contains("FlatUI.Library") == true)
                {
                    resourcesToReload.Add(dict);
                }
            }

            // 移除并重新添加资源字典以强制刷新
            foreach (var dict in resourcesToReload)
            {
                window.Resources.MergedDictionaries.Remove(dict);
                
                var newDict = new ResourceDictionary { Source = dict.Source };
                window.Resources.MergedDictionaries.Add(newDict);
            }
        }

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
        /// 获取动态资源
        /// </summary>
        public object GetResource(string resourceKey)
        {
            try
            {
                var resourceManager = new ResourceManager("FlatUI.Library.Resources.Resources", typeof(LanguageManager).Assembly);
                return resourceManager?.GetObject(resourceKey, CurrentCulture) ?? resourceKey;
            }
            catch
            {
                return resourceKey;
            }
        }

        /// <summary>
        /// 切换到中文
        /// </summary>
        public void SwitchToChinese()
        {
            CurrentCulture = CultureInfo.GetCultureInfo("zh-CN");
            NotifyLanguageChanged();
        }

        /// <summary>
        /// 切换到英文
        /// </summary>
        public void SwitchToEnglish()
        {
            CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            NotifyLanguageChanged();
        }

        /// <summary>
        /// 检查是否支持指定语言
        /// </summary>
        public bool IsSupportedLanguage(string cultureName)
        {
            foreach (var culture in SupportedCultures)
            {
                if (culture.Name.Equals(cultureName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 获取当前语言名称
        /// </summary>
        public string GetCurrentLanguageName()
        {
            return CurrentCulture.Name switch
            {
                "zh-CN" => "简体中文",
                "en-US" => "English",
                _ => CurrentCulture.DisplayName
            };
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

        /// <summary>
        /// 获取本地化资源
        /// </summary>
        public static object LangResource(this string key)
        {
            return LanguageManager.Instance.GetResource(key);
        }
    }

    /// <summary>
    /// 本地化标记扩展 - 用于 XAML 直接绑定
    /// </summary>
    public class LocalizeExtension : MarkupExtension
    {
        public string Key { get; set; }

        public LocalizeExtension() { }

        public LocalizeExtension(string key)
        {
            Key = key;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return LanguageManager.Instance.GetString(Key);
        }
    }
}
