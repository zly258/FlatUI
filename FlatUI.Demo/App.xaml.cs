using System.Configuration;
using System.Data;
using System.Windows;
using FlatUI.Library.Controls;

namespace FlatUI.Demo;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        // 初始化语言管理器（默认中文）
        LanguageManager.Instance.SwitchToChinese();
    }
}

