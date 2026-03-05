using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace FlatUI.Demo.Views
{
    public partial class ChartView : UserControl
    {
        public ChartView()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            // 折线图数据
            LineChart1.ItemsSource = new List<double> { 65, 59, 80, 81, 56, 55, 40, 70, 65, 85, 90, 75 };
            
            // 柱状图数据
            BarChart1.ItemsSource = new List<double> { 120, 200, 150, 80, 70, 110, 130, 90, 100, 140, 160, 180 };
            
            // 面积图数据
            AreaChart1.ItemsSource = new List<double> { 45, 52, 38, 65, 48, 72, 58, 80, 65, 90, 75, 85 };
            
            // 饼图数据
            PieChart1.ItemsSource = new List<double> { 30, 25, 20, 15, 10 };
            
            // 环形图数据
            DonutChart1.ItemsSource = new List<double> { 40, 30, 20, 10 };
        }
    }
}
