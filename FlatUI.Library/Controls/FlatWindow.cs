using System.Windows;
using System.Windows.Input;

namespace FlatUI.Library.Controls
{
    public class FlatWindow : Window
    {
        static FlatWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatWindow), new FrameworkPropertyMetadata(typeof(FlatWindow)));
        }

        public FlatWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, OnCloseWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, OnMaximizeWindow, OnCanResizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, OnMinimizeWindow, OnCanMinimizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, OnRestoreWindow, OnCanResizeWindow));
        }

        private void OnCanResizeWindow(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;
        private void OnCanMinimizeWindow(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;

        private void OnCloseWindow(object sender, ExecutedRoutedEventArgs e) => SystemCommands.CloseWindow(this);
        private void OnMaximizeWindow(object sender, ExecutedRoutedEventArgs e) => SystemCommands.MaximizeWindow(this);
        private void OnMinimizeWindow(object sender, ExecutedRoutedEventArgs e) => SystemCommands.MinimizeWindow(this);
        private void OnRestoreWindow(object sender, ExecutedRoutedEventArgs e) => SystemCommands.RestoreWindow(this);
    }
}