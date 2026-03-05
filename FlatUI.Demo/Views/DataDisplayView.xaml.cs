using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FlatUI.Library.Controls;

namespace FlatUI.Demo.Views
{
    public partial class DataDisplayView : UserControl
    {
        private List<TableItem> _filterData = new List<TableItem>();
        private List<TableItem> _multiSelectData = new List<TableItem>();

        public DataDisplayView()
        {
            InitializeComponent();
            LoadFilterData();
            LoadMultiSelectData();
        }

        private void LoadFilterData()
        {
            _filterData = new List<TableItem>
            {
                new TableItem { Id = 1, Name = "John Doe", Status = "Active", Value = 100 },
                new TableItem { Id = 2, Name = "Jane Smith", Status = "Inactive", Value = 200 },
                new TableItem { Id = 3, Name = "Bob Johnson", Status = "Active", Value = 150 },
                new TableItem { Id = 4, Name = "Alice Brown", Status = "Active", Value = 180 },
                new TableItem { Id = 5, Name = "Charlie Wilson", Status = "Inactive", Value = 220 }
            };
            FilterDataGrid.ItemsSource = _filterData;
        }

        private void LoadMultiSelectData()
        {
            _multiSelectData = new List<TableItem>
            {
                new TableItem { Id = 1, Name = "John Doe", Email = "john@example.com", Department = "IT" },
                new TableItem { Id = 2, Name = "Jane Smith", Email = "jane@example.com", Department = "HR" },
                new TableItem { Id = 3, Name = "Bob Johnson", Email = "bob@example.com", Department = "IT" },
                new TableItem { Id = 4, Name = "Alice Brown", Email = "alice@example.com", Department = "Finance" },
                new TableItem { Id = 5, Name = "Charlie Wilson", Email = "charlie@example.com", Department = "HR" }
            };
            MultiSelectGrid.ItemsSource = _multiSelectData;
            SingleSelectGrid.ItemsSource = _multiSelectData;
        }

        private void FilterCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilter();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            if (FilterCombo == null || SearchBox == null || FilterDataGrid == null)
                return;
                
            var filtered = _filterData.AsEnumerable();
            
            if (FilterCombo.SelectedIndex > 0)
            {
                var status = FilterCombo.SelectedIndex == 1 ? "Active" : "Inactive";
                filtered = filtered.Where(x => x.Status == status);
            }
            
            if (!string.IsNullOrWhiteSpace(SearchBox.Text))
            {
                var search = SearchBox.Text.ToLower();
                filtered = filtered.Where(x => x.Name.ToLower().Contains(search));
            }
            
            FilterDataGrid.ItemsSource = filtered.ToList();
        }

        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            if (SelectAllCheck.IsChecked == true)
            {
                MultiSelectGrid.SelectAll();
            }
            else
            {
                MultiSelectGrid.UnselectAll();
            }
        }

        private void GetSingleSelected_Click(object sender, RoutedEventArgs e)
        {
            var selected = SingleSelectGrid.SelectedItem as TableItem;
            if (selected != null)
            {
                NotificationService.Show("选中项", $"ID: {selected.Id}, 姓名: {selected.Name}", StatusType.Info);
            }
        }

        private void GetMultiSelected_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = MultiSelectGrid.SelectedItems.Cast<TableItem>().ToList();
            if (selectedItems.Any())
            {
                var names = string.Join(", ", selectedItems.Select(x => x.Name));
                NotificationService.Show("选中项", $"数量: {selectedItems.Count}, 姓名: {names}", StatusType.Info);
            }
        }

        private void LoadLargeData_Click(object sender, RoutedEventArgs e)
        {
            var list = new List<object>();
            var rand = new Random();
            for (int i = 1; i <= 10000; i++)
            {
                list.Add(new { 
                    Id = i, 
                    Name = $"Sensor_Node_{i:D5}", 
                    Timestamp = DateTime.Now.AddSeconds(-i).ToString("yyyy-MM-dd HH:mm:ss"),
                    Value = rand.NextDouble() * 100
                });
            }
            BigDataGrid.ItemsSource = list;
        }
    }

    public class TableItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Status { get; set; } = "";
        public string Department { get; set; } = "";
        public double Value { get; set; }
    }
}
