using System.Windows;

namespace FlatUI.Library.Controls
{
    public partial class FlatMessageBoxWindow : Window
    {
        public FlatMessageBoxWindow(string message, string title = "Message")
        {
            InitializeComponent();
            MessageText.Text = message;
            TitleText.Text = title;
        }

        private void Close_Click(object sender, RoutedEventArgs e) => Close();
        private void Ok_Click(object sender, RoutedEventArgs e) { DialogResult = true; Close(); }
        private void Cancel_Click(object sender, RoutedEventArgs e) { DialogResult = false; Close(); }
    }

    public static class FlatMessageBox
    {
        public static bool? Show(string message, string title = "Message", Window? owner = null)
        {
            var win = new FlatMessageBoxWindow(message, title);
            if (owner != null) win.Owner = owner;
            return win.ShowDialog();
        }
    }
}