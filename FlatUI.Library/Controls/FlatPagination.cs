using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FlatUI.Library.Controls
{
    /// <summary>
    /// 分页控件
    /// </summary>
    public class FlatPagination : System.Windows.Controls.Control
    {
        static FlatPagination()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatPagination), new FrameworkPropertyMetadata(typeof(FlatPagination)));
        }

        public static readonly DependencyProperty PageIndexProperty =
            DependencyProperty.Register("PageIndex", typeof(int), typeof(FlatPagination), new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPageIndexChanged));

        public int PageIndex
        {
            get { return (int)GetValue(PageIndexProperty); }
            set { SetValue(PageIndexProperty, value); }
        }

        public static readonly DependencyProperty TotalCountProperty =
            DependencyProperty.Register("TotalCount", typeof(int), typeof(FlatPagination), new PropertyMetadata(0, OnTotalCountChanged));

        public int TotalCount
        {
            get { return (int)GetValue(TotalCountProperty); }
            set { SetValue(TotalCountProperty, value); }
        }

        public static readonly DependencyProperty PageSizeProperty =
            DependencyProperty.Register("PageSize", typeof(int), typeof(FlatPagination), new PropertyMetadata(10, OnTotalCountChanged));

        public int PageSize
        {
            get { return (int)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }

        public int TotalPages => TotalCount <= 0 ? 1 : (int)Math.Ceiling((double)TotalCount / PageSize);

        public static readonly DependencyProperty ShowTotalProperty =
            DependencyProperty.Register("ShowTotal", typeof(bool), typeof(FlatPagination), new PropertyMetadata(true));

        public bool ShowTotal
        {
            get { return (bool)GetValue(ShowTotalProperty); }
            set { SetValue(ShowTotalProperty, value); }
        }

        public static readonly DependencyProperty ShowQuickJumperProperty =
            DependencyProperty.Register("ShowQuickJumper", typeof(bool), typeof(FlatPagination), new PropertyMetadata(false));

        public static readonly DependencyProperty ShowSizeChangerProperty = DependencyProperty.Register("ShowSizeChanger", typeof(bool), typeof(FlatPagination), new PropertyMetadata(false));

        public bool ShowSizeChanger
        {
            get => (bool)GetValue(ShowSizeChangerProperty);
            set => SetValue(ShowSizeChangerProperty, value);
        }

        public bool ShowQuickJumper
        {
            get => (bool)GetValue(ShowQuickJumperProperty);
            set => SetValue(ShowQuickJumperProperty, value);
        }

        private static void OnPageIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FlatPagination p) p.UpdatePages();
        }

        private static void OnTotalCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FlatPagination p) p.UpdatePages();
        }

        public ObservableCollection<int> Pages { get; } = new ObservableCollection<int>();

        private void UpdatePages()
        {
            Pages.Clear();
            int total = TotalPages;
            int current = PageIndex;
            
            // 保持显示 5 个页码
            int start = Math.Max(1, current - 2);
            int end = Math.Min(total, start + 4);
            if (end - start < 4) start = Math.Max(1, end - 4);
            
            for (int i = start; i <= end; i++) Pages.Add(i);
        }

        public ICommand PrevCommand => new RelayCommand(_ => PageIndex = Math.Max(1, PageIndex - 1));
        public ICommand NextCommand => new RelayCommand(_ => PageIndex = Math.Min(TotalPages, PageIndex + 1));
        public ICommand PageCommand => new RelayCommand(p => {
            if (p is int i) PageIndex = i;
        });

        public ICommand JumpCommand => new RelayCommand(p => {
            if (p != null && int.TryParse(p.ToString(), out int page))
            {
                PageIndex = Math.Max(1, Math.Min(TotalPages, page));
            }
        });
    }
}