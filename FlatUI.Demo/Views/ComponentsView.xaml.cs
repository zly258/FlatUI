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
            var data = new List<double> { 12, 45, 23, 89, 56, 72, 34, 61, 95, 28 };
            LineChart.ItemsSource = data;
            BarChart.ItemsSource = data;
        }
    }
}