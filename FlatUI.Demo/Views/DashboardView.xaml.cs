using System.Windows;
using System.Windows.Controls;
using FlatUI.Library.Controls;

namespace FlatUI.Demo.Views
{
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();
        }

        private void OpenDrawer_Click(object sender, RoutedEventArgs e)
        {
            var win = Window.GetWindow(this) as MainWindow;
            if (win != null) win.SideDrawer.IsOpen = true;
        }

        private void TestDialog_Click(object sender, RoutedEventArgs e)
        {
            FlatMessageBox.Show("This is a dashboard test message.", "Notice", Window.GetWindow(this));
        }
    }
}