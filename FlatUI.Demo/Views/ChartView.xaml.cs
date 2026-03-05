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
            LineChart1.ItemsSource = new System.Collections.ObjectModel.ObservableCollection<FlatUI.Library.Controls.ChartDataItem>
            {
                new() { Label = "1月", Value = 65 },
                new() { Label = "2月", Value = 59 },
                new() { Label = "3月", Value = 80 },
                new() { Label = "4月", Value = 81 },
                new() { Label = "5月", Value = 56 },
                new() { Label = "6月", Value = 55 },
                new() { Label = "7月", Value = 40 },
                new() { Label = "8月", Value = 70 },
                new() { Label = "9月", Value = 65 },
                new() { Label = "10月", Value = 85 },
                new() { Label = "11月", Value = 90 },
                new() { Label = "12月", Value = 75 }
            };
            
            // 柱状图数据
            BarChart1.ItemsSource = new System.Collections.ObjectModel.ObservableCollection<FlatUI.Library.Controls.ChartDataItem>
            {
                new() { Label = "产品A", Value = 120 },
                new() { Label = "产品B", Value = 200 },
                new() { Label = "产品C", Value = 150 },
                new() { Label = "产品D", Value = 80 },
                new() { Label = "产品E", Value = 70 },
                new() { Label = "产品F", Value = 110 },
                new() { Label = "产品G", Value = 130 },
                new() { Label = "产品H", Value = 90 },
                new() { Label = "产品I", Value = 100 },
                new() { Label = "产品J", Value = 140 },
                new() { Label = "产品K", Value = 160 },
                new() { Label = "产品L", Value = 180 }
            };
            
            // 面积图数据
            AreaChart1.ItemsSource = new System.Collections.ObjectModel.ObservableCollection<FlatUI.Library.Controls.ChartDataItem>
            {
                new() { Label = "周一", Value = 45 },
                new() { Label = "周二", Value = 52 },
                new() { Label = "周三", Value = 38 },
                new() { Label = "周四", Value = 65 },
                new() { Label = "周五", Value = 48 },
                new() { Label = "周六", Value = 72 },
                new() { Label = "周日", Value = 58 }
            };
            
            // 饼图数据
            PieChart1.ItemsSource = new System.Collections.ObjectModel.ObservableCollection<FlatUI.Library.Controls.ChartDataItem>
            {
                new() { Label = "苹果", Value = 30 },
                new() { Label = "三星", Value = 25 },
                new() { Label = "华为", Value = 20 },
                new() { Label = "小米", Value = 15 },
                new() { Label = "其他", Value = 10 }
            };
            
            // 环形图数据
            DonutChart1.ItemsSource = new System.Collections.ObjectModel.ObservableCollection<FlatUI.Library.Controls.ChartDataItem>
            {
                new() { Label = "男性", Value = 40 },
                new() { Label = "女性", Value = 30 },
                new() { Label = "未知", Value = 20 },
                new() { Label = "其他", Value = 10 }
            };
        }
    }
}
