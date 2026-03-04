using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace FlatUI.Library.Controls
{
    public static class TrayManager
    {
        private static NotifyIcon? _notifyIcon;

        public static void Initialize(string title, Icon icon)
        {
            if (_notifyIcon != null) return;

            _notifyIcon = new NotifyIcon
            {
                Icon = icon,
                Text = title,
                Visible = true
            };

            _notifyIcon.DoubleClick += (s, e) => {
                if (System.Windows.Application.Current.MainWindow != null)
                {
                    System.Windows.Application.Current.MainWindow.Show();
                    System.Windows.Application.Current.MainWindow.WindowState = System.Windows.WindowState.Normal;
                    System.Windows.Application.Current.MainWindow.Activate();
                }
            };
        }

        public static void SetContextMenu(ContextMenuStrip contextMenu)
        {
            if (_notifyIcon != null) _notifyIcon.ContextMenuStrip = contextMenu;
        }

        public static void ShowNotification(string title, string text, ToolTipIcon icon = ToolTipIcon.Info)
        {
            if (_notifyIcon != null) _notifyIcon.ShowBalloonTip(3000, title, text, icon);
        }

        public static void Dispose()
        {
            if (_notifyIcon != null)
            {
                _notifyIcon.Visible = false;
                _notifyIcon.Dispose();
                _notifyIcon = null;
            }
        }
    }
}