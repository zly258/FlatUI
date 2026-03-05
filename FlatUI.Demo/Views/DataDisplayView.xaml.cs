using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace FlatUI.Demo.Views
{
    public partial class DataDisplayView : UserControl
    {
        public DataDisplayView()
        {
            InitializeComponent();
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
}