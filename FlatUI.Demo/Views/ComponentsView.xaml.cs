using System.Collections.Generic;
using System.Windows.Controls;
using FlatUI.Library.Controls;

namespace FlatUI.Demo.Views
{
    public partial class ComponentsView : UserControl
    {
        public ComponentsView()
        {
            InitializeComponent();
            LoadChartData();
        }

        private void LoadChartData()
        {
            var data = new System.Collections.ObjectModel.ObservableCollection<ChartDataItem>
            {
                new() { Label = "第1天", Value = 12 },
                new() { Label = "第2天", Value = 45 },
                new() { Label = "第3天", Value = 23 },
                new() { Label = "第4天", Value = 89 },
                new() { Label = "第5天", Value = 56 },
                new() { Label = "第6天", Value = 72 },
                new() { Label = "第7天", Value = 34 },
                new() { Label = "第8天", Value = 61 },
                new() { Label = "第9天", Value = 95 },
                new() { Label = "第10天", Value = 28 }
            };
            
            LineChart.ItemsSource = data;
            BarChart.ItemsSource = data;
        }
    }
}