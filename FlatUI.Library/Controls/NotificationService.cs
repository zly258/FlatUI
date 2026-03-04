using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FlatUI.Library.Controls
{
    public static class NotificationService
    {
        private static StackPanel? _notificationPanel;

        public static void RegisterHost(Window window)
        {
            if (VisualTreeHelper.GetChild(window, 0) is not Decorator decorator) return;
            if (decorator.Child is not Panel originalContent) return;

            var grid = new Grid();
            decorator.Child = grid;
            grid.Children.Add(originalContent);

            _notificationPanel = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0, 0, 20, 20)
            };
            grid.Children.Add(_notificationPanel);
        }

        public static void Show(string title, string message, StatusType type = StatusType.Info, int durationSeconds = 5)
        {
            if (_notificationPanel == null) return;

            var notification = new NotificationControl(title, message, type, durationSeconds);
            notification.Closed += (n) => _notificationPanel.Children.Remove(n);
            
            _notificationPanel.Children.Add(notification);
        }
    }
}