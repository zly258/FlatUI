using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace FlatUI.Library.Controls
{
    public static class TrayManager
    {
        private static NotifyIcon? _notifyIcon;
        private static System.Windows.Threading.DispatcherTimer? _flashTimer;
        private static Icon? _originalIcon;
        private static bool _isFlashing;

        public static void Initialize(string title, Icon icon)
        {
            if (_notifyIcon != null) return;

            _originalIcon = icon;
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

        public static void SetIcon(Icon icon)
        {
            if (_notifyIcon != null)
            {
                _originalIcon = icon;
                if (!_isFlashing) _notifyIcon.Icon = icon;
            }
        }

        public static void StartFlashing(Icon? flashIcon = null)
        {
            if (_notifyIcon == null || _isFlashing) return;

            _isFlashing = true;
            bool showOriginal = false;
            _flashTimer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };

            _flashTimer.Tick += (s, e) =>
            {
                if (_notifyIcon == null) return;
                _notifyIcon.Icon = showOriginal ? _originalIcon : (flashIcon ?? new Icon(SystemIcons.Warning, 16, 16));
                showOriginal = !showOriginal;
            };
            _flashTimer.Start();
        }

        public static void StopFlashing()
        {
            if (!_isFlashing) return;
            _flashTimer?.Stop();
            _isFlashing = false;
            if (_notifyIcon != null) _notifyIcon.Icon = _originalIcon;
        }

        public static void SetContextMenu(ContextMenuStrip contextMenu)
        {
            if (_notifyIcon != null) _notifyIcon.ContextMenuStrip = contextMenu;
        }

        public static void ShowBalloonTip(string title, string text, ToolTipIcon icon = ToolTipIcon.Info, int timeout = 3000)
        {
            if (_notifyIcon != null)
            {
                _notifyIcon.ShowBalloonTip(timeout, title, text, icon);
            }
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