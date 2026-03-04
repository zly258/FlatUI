using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FlatUI.Library.Controls
{
    public class Pagination : Control
    {
        static Pagination()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pagination), new FrameworkPropertyMetadata(typeof(Pagination)));
        }

        public static readonly DependencyProperty PageIndexProperty =
            DependencyProperty.Register("PageIndex", typeof(int), typeof(Pagination), new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPageIndexChanged));

        public int PageIndex
        {
            get { return (int)GetValue(PageIndexProperty); }
            set { SetValue(PageIndexProperty, value); }
        }

        public static readonly DependencyProperty TotalCountProperty =
            DependencyProperty.Register("TotalCount", typeof(int), typeof(Pagination), new PropertyMetadata(0, OnTotalCountChanged));

        public int TotalCount
        {
            get { return (int)GetValue(TotalCountProperty); }
            set { SetValue(TotalCountProperty, value); }
        }

        public static readonly DependencyProperty PageSizeProperty =
            DependencyProperty.Register("PageSize", typeof(int), typeof(Pagination), new PropertyMetadata(10, OnTotalCountChanged));

        public int PageSize
        {
            get { return (int)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }

        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        private static void OnPageIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Pagination p) p.UpdatePages();
        }

        private static void OnTotalCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Pagination p) p.UpdatePages();
        }

        public ObservableCollection<int> Pages { get; } = new ObservableCollection<int>();

        private void UpdatePages()
        {
            Pages.Clear();
            int start = Math.Max(1, PageIndex - 2);
            int end = Math.Min(TotalPages, start + 4);
            if (end - start < 4) start = Math.Max(1, end - 4);
            
            for (int i = start; i <= end; i++) Pages.Add(i);
        }

        public ICommand PrevCommand => new RelayCommand(_ => PageIndex = Math.Max(1, PageIndex - 1));
        public ICommand NextCommand => new RelayCommand(_ => PageIndex = Math.Min(TotalPages, PageIndex + 1));
        public ICommand PageCommand => new RelayCommand(p => PageIndex = (int)p);
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        public RelayCommand(Action<object> execute) => _execute = execute;
        public bool CanExecute(object? parameter) => true;
        public void Execute(object? parameter) => _execute(parameter!);
        public event EventHandler? CanExecuteChanged;
    }
}