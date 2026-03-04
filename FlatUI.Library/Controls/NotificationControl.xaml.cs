using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace FlatUI.Library.Controls
{
    public partial class NotificationControl : UserControl
    {
        public event Action<NotificationControl>? Closed;

        public NotificationControl(string title, string message, StatusType type = StatusType.Info, int durationSeconds = 5)
        {
            InitializeComponent();
            TitleText.Text = title;
            MessageText.Text = message;

            Icon.Fill = GetBrushForStatus(type);

            if (durationSeconds > 0)
            {
                var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(durationSeconds) };
                timer.Tick += (s, e) => { timer.Stop(); Close(); };
                timer.Start();
            }
        }

        private Brush GetBrushForStatus(StatusType type)
        {
            return type switch
            {
                StatusType.Success => (Brush)FindResource("SuccessBrush"),
                StatusType.Warning => (Brush)FindResource("WarningBrush"),
                StatusType.Error => (Brush)FindResource("ErrorBrush"),
                _ => (Brush)FindResource("InfoBrush"),
            };
        }

        private void Close_Click(object sender, RoutedEventArgs e) => Close();

        private void Close() => Closed?.Invoke(this);
    }
}