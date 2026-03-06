using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Control = System.Windows.Controls.Control;
using ToolTip = System.Windows.Controls.ToolTip;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace FlatUI.Library.Controls
{
    /// <summary>
    /// 扁平化图表控件 - 支持悬浮提示、轴刻度、标题等完整功能
    /// </summary>
    public class FlatChart : Control
    {
        static FlatChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatChart), new FrameworkPropertyMetadata(typeof(FlatChart)));
        }

        #region 依赖属性

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<ChartDataItem>), typeof(FlatChart), 
                new PropertyMetadata(new ObservableCollection<ChartDataItem>(), OnDataChanged));

        public ObservableCollection<ChartDataItem> ItemsSource
        {
            get => (ObservableCollection<ChartDataItem>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly DependencyProperty ChartTypeProperty =
            DependencyProperty.Register("ChartType", typeof(ChartType), typeof(FlatChart), 
                new PropertyMetadata(ChartType.Line, OnChartTypeChanged));

        public ChartType ChartType
        {
            get => (ChartType)GetValue(ChartTypeProperty);
            set => SetValue(ChartTypeProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(FlatChart), 
                new PropertyMetadata("图表标题"));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty ShowGridProperty =
            DependencyProperty.Register("ShowGrid", typeof(bool), typeof(FlatChart), 
                new PropertyMetadata(true));

        public bool ShowGrid
        {
            get => (bool)GetValue(ShowGridProperty);
            set => SetValue(ShowGridProperty, value);
        }

        public static readonly DependencyProperty ShowLegendProperty =
            DependencyProperty.Register("ShowLegend", typeof(bool), typeof(FlatChart), 
                new PropertyMetadata(true));

        public bool ShowLegend
        {
            get => (bool)GetValue(ShowLegendProperty);
            set => SetValue(ShowLegendProperty, value);
        }

        public static readonly DependencyProperty ShowTooltipProperty =
            DependencyProperty.Register("ShowTooltip", typeof(bool), typeof(FlatChart), 
                new PropertyMetadata(true));

        public bool ShowTooltip
        {
            get => (bool)GetValue(ShowTooltipProperty);
            set => SetValue(ShowTooltipProperty, value);
        }

        #endregion

        #region 事件

        public static readonly RoutedEvent TooltipShowingEvent = 
            EventManager.RegisterRoutedEvent("TooltipShowing", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FlatChart));

        public event RoutedEventHandler TooltipShowing
        {
            add => AddHandler(TooltipShowingEvent, value);
            remove => RemoveHandler(TooltipShowingEvent, value);
        }

        #endregion

        private Canvas? _chartCanvas;
        private ToolTip? _tooltip;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            _chartCanvas = GetTemplateChild("PART_ChartCanvas") as Canvas;
            _tooltip = new ToolTip { Style = (Style)FindResource("FlatTooltip") };
            
            if (_chartCanvas != null)
            {
                _chartCanvas.MouseMove += OnChartMouseMove;
                _chartCanvas.MouseLeave += OnChartMouseLeave;
            }
            
            UpdateChart();
        }

        private static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FlatChart chart) chart.UpdateChart();
        }

        private static void OnChartTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FlatChart chart) chart.UpdateChart();
        }

        private void UpdateChart()
        {
            if (_chartCanvas == null) return;
            
            _chartCanvas.Children.Clear();
            
            if (ItemsSource == null || !ItemsSource.Any()) return;
            
            DrawChart();
        }

        private void DrawChart()
        {
            double width = _chartCanvas!.ActualWidth;
            double height = _chartCanvas.ActualHeight;
            
            if (width < 50 || height < 50) return;
            
            // 绘制标题
            DrawTitle(width, height);
            
            // 绘制数据
            switch (ChartType)
            {
                case ChartType.Line:
                case ChartType.Area:
                    // 对于折线图和面积图，绘制坐标轴和网格
                    DrawAxes(width, height);
                    if (ShowGrid) DrawGrid(width, height);
                    DrawLineChart(width, height);
                    break;
                case ChartType.Bar:
                    // 对于柱状图，绘制坐标轴和网格
                    DrawAxes(width, height);
                    if (ShowGrid) DrawGrid(width, height);
                    DrawBarChart(width, height);
                    break;
                case ChartType.Pie:
                    DrawPieChart(width, height);
                    break;
                case ChartType.Donut:
                    DrawDonutChart(width, height);
                    break;
            }
        }

        private void DrawTitle(double width, double height)
        {
            var titleText = new TextBlock
            {
                Text = Title,
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)FindResource("TextBrush"),
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center
            };
            
            Canvas.SetLeft(titleText, width / 2 - titleText.ActualWidth / 2);
            Canvas.SetTop(titleText, 5);
            _chartCanvas!.Children.Add(titleText);
        }

        private void DrawAxes(double width, double height)
        {
            double margin = 60;
            
            // X轴
            var xAxis = new Line
            {
                X1 = margin,
                Y1 = height - margin,
                X2 = width - margin,
                Y2 = height - margin,
                Stroke = (Brush)FindResource("TextSecondaryBrush"),
                StrokeThickness = 1
            };
            _chartCanvas!.Children.Add(xAxis);
            
            // Y轴
            var yAxis = new Line
            {
                X1 = margin,
                Y1 = margin,
                X2 = margin,
                Y2 = height - margin,
                Stroke = (Brush)FindResource("TextSecondaryBrush"),
                StrokeThickness = 1
            };
            _chartCanvas.Children.Add(yAxis);
        }

        private void DrawGrid(double width, double height)
        {
            double margin = 60;
            double chartWidth = width - margin * 2;
            double chartHeight = height - margin * 2;
            
            // 水平网格线
            for (int i = 0; i <= 5; i++)
            {
                double y = margin + (chartHeight / 5) * i;
                var gridLine = new Line
                {
                    X1 = margin,
                    Y1 = y,
                    X2 = width - margin,
                    Y2 = y,
                    Stroke = (Brush)FindResource("RegionBrush"),
                    StrokeThickness = 0.5
                };
                _chartCanvas!.Children.Add(gridLine);
            }
        }

        private void DrawLineChart(double width, double height)
        {
            double margin = 60;
            double chartWidth = width - margin * 2;
            double chartHeight = height - margin * 2;
            
            if (ItemsSource == null || !ItemsSource.Any()) return;
            double maxValue = ItemsSource.Max(item => item.Value);
            if (maxValue == 0) maxValue = 1;
            
            var points = new List<System.Windows.Point>();
            
            for (int i = 0; i < ItemsSource.Count; i++)
            {
                double x = margin + (chartWidth / (ItemsSource.Count - 1)) * i;
                double y = height - margin - (ItemsSource[i].Value / maxValue) * chartHeight;
                points.Add(new System.Windows.Point(x, y));
                
                // 绘制数据点
                var ellipse = new Ellipse
                {
                    Width = 6,
                    Height = 6,
                    Fill = (Brush)FindResource("PrimaryBrush"),
                    Stroke = Brushes.White,
                    StrokeThickness = 1,
                    Tag = ItemsSource[i]
                };
                Canvas.SetLeft(ellipse, x - 3);
                Canvas.SetTop(ellipse, y - 3);
                _chartCanvas!.Children.Add(ellipse);
            }
            
            // 绘制连线
            var polyline = new Polyline
            {
                Points = new PointCollection(points),
                Stroke = (Brush)FindResource("PrimaryBrush"),
                StrokeThickness = 2,
                StrokeLineJoin = PenLineJoin.Round
            };
            _chartCanvas.Children.Add(polyline);
        }

        private void DrawBarChart(double width, double height)
        {
            double margin = 60;
            double chartWidth = width - margin * 2;
            double chartHeight = height - margin * 2;
            
            double maxValue = ItemsSource.Max(item => item.Value);
            if (maxValue == 0) maxValue = 1;
            
            double barWidth = (chartWidth / ItemsSource.Count) * 0.6;
            double spacing = chartWidth / ItemsSource.Count;
            
            for (int i = 0; i < ItemsSource.Count; i++)
            {
                double barHeight = (ItemsSource[i].Value / maxValue) * chartHeight;
                double x = margin + (i * spacing) + (spacing - barWidth) / 2;
                double y = height - margin - barHeight;
                
                var rectangle = new Rectangle
                {
                    Width = barWidth,
                    Height = barHeight,
                    Fill = (Brush)FindResource("PrimaryBrush"),
                    Tag = ItemsSource[i]
                };
                Canvas.SetLeft(rectangle, x);
                Canvas.SetTop(rectangle, y);
                _chartCanvas!.Children.Add(rectangle);
            }
        }

        private void DrawPieChart(double width, double height)
        {
            double centerX = width / 2;
            double centerY = height / 2;
            double radius = Math.Min(width, height) / 2 - 20;
            
            double total = ItemsSource.Sum(item => item.Value);
            if (total == 0) return;
            
            double startAngle = 0;
            var colors = GetChartColors();
            
            for (int i = 0; i < ItemsSource.Count; i++)
            {
                double sweepAngle = (ItemsSource[i].Value / total) * 360;
                
                // 绘制扇形
                var path = new Path
                {
                    Fill = colors[i % colors.Count],
                    Stroke = Brushes.White,
                    StrokeThickness = 1,
                    Tag = ItemsSource[i]
                };
                
                var geometry = CreateArcGeometry(centerX, centerY, radius, radius, startAngle, sweepAngle);
                path.Data = geometry;
                
                _chartCanvas!.Children.Add(path);
                startAngle += sweepAngle;
            }
        }

        private void DrawDonutChart(double width, double height)
        {
            double centerX = width / 2;
            double centerY = height / 2;
            double outerRadius = Math.Min(width, height) / 2 - 20;
            double innerRadius = outerRadius * 0.6;
            
            double total = ItemsSource.Sum(item => item.Value);
            if (total == 0) return;
            
            double startAngle = 0;
            var colors = GetChartColors();
            
            for (int i = 0; i < ItemsSource.Count; i++)
            {
                double sweepAngle = (ItemsSource[i].Value / total) * 360;
                
                // 绘制环形扇形
                var path = new Path
                {
                    Fill = colors[i % colors.Count],
                    Stroke = Brushes.White,
                    StrokeThickness = 1,
                    Tag = ItemsSource[i]
                };
                
                var geometry = CreateDonutGeometry(centerX, centerY, outerRadius, innerRadius, startAngle, sweepAngle);
                path.Data = geometry;
                
                _chartCanvas!.Children.Add(path);
                startAngle += sweepAngle;
            }
        }

        private StreamGeometry CreateArcGeometry(double cx, double cy, double rx, double ry, double startAngle, double sweepAngle)
        {
            var geometry = new StreamGeometry();
            using (var ctx = geometry.Open())
            {
                double startRad = startAngle * Math.PI / 180;
                double endRad = (startAngle + sweepAngle) * Math.PI / 180;
                
                var startPoint = new System.Windows.Point(cx + rx * Math.Cos(startRad), cy + ry * Math.Sin(startRad));
                var endPoint = new System.Windows.Point(cx + rx * Math.Cos(endRad), cy + ry * Math.Sin(endRad));
                
                ctx.BeginFigure(new System.Windows.Point(cx, cy), false, false);
                ctx.LineTo(startPoint, true, false);
                ctx.ArcTo(endPoint, new System.Windows.Size(rx, ry), 0, sweepAngle > 180, SweepDirection.Clockwise, true, false);
                ctx.LineTo(new System.Windows.Point(cx, cy), true, false);
                ctx.Close();
            }
            return geometry;
        }

        private StreamGeometry CreateDonutGeometry(double cx, double cy, double outerR, double innerR, double startAngle, double sweepAngle)
        {
            var geometry = new StreamGeometry();
            using (var ctx = geometry.Open())
            {
                double startRad = startAngle * Math.PI / 180;
                double endRad = (startAngle + sweepAngle) * Math.PI / 180;
                
                var outerStart = new System.Windows.Point(cx + outerR * Math.Cos(startRad), cy + outerR * Math.Sin(startRad));
                var outerEnd = new System.Windows.Point(cx + outerR * Math.Cos(endRad), cy + outerR * Math.Sin(endRad));
                var innerStart = new System.Windows.Point(cx + innerR * Math.Cos(startRad), cy + innerR * Math.Sin(startRad));
                var innerEnd = new System.Windows.Point(cx + innerR * Math.Cos(endRad), cy + innerR * Math.Sin(endRad));
                
                ctx.BeginFigure(outerStart, false, false);
                ctx.ArcTo(outerEnd, new System.Windows.Size(outerR, outerR), 0, sweepAngle > 180, SweepDirection.Clockwise, true, false);
                ctx.LineTo(innerEnd, true, false);
                ctx.ArcTo(innerStart, new System.Windows.Size(innerR, innerR), 0, sweepAngle > 180, SweepDirection.Counterclockwise, true, false);
                ctx.Close();
            }
            return geometry;
        }

        private List<Brush> GetChartColors()
        {
            return new List<Brush>
            {
                (Brush)FindResource("PrimaryBrush"),
                (Brush)FindResource("SuccessBrush"),
                (Brush)FindResource("WarningBrush"),
                (Brush)FindResource("ErrorBrush"),
                (Brush)FindResource("InfoBrush"),
                (Brush)FindResource("PurpleBrush"),
                (Brush)FindResource("CyanBrush"),
                (Brush)FindResource("OrangeBrush")
            };
        }

        private void OnChartMouseMove(object sender, MouseEventArgs e)
        {
            if (!ShowTooltip || _chartCanvas == null) return;
            
            var position = e.GetPosition(_chartCanvas);
            var hitElement = _chartCanvas.InputHitTest(position) as FrameworkElement;
            
            if (hitElement?.Tag is ChartDataItem dataItem)
            {
                var args = new RoutedEventArgs(TooltipShowingEvent, this);
                RaiseEvent(args);
                
                _tooltip!.Content = $"{dataItem.Label}: {dataItem.Value}";
                _tooltip.IsOpen = true;
            }
            else
            {
                _tooltip!.IsOpen = false;
            }
        }

        private void OnChartMouseLeave(object sender, MouseEventArgs e)
        {
            _tooltip!.IsOpen = false;
        }
    }

    /// <summary>
    /// 图表类型枚举
    /// </summary>
    public enum ChartType
    {
        Line,
        Bar,
        Pie,
        Donut,
        Area
    }

    /// <summary>
    /// 图表数据项
    /// </summary>
    public class ChartDataItem
    {
        public string Label { get; set; } = string.Empty;
        public double Value { get; set; }
        public object? Tag { get; set; }
    }
}