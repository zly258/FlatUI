using System;
using System.Collections.Generic;
using System.Linq;
using WpfPoint = System.Windows.Point;
using WpfRect = System.Windows.Rect;
using WpfSize = System.Windows.Size;
using WpfPen = System.Windows.Media.Pen;
using WpfColor = System.Windows.Media.Color;
using WpfBrushes = System.Windows.Media.Brushes;
using WpfApplication = System.Windows.Application;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FlatUI.Library.Controls
{
    /// <summary>
    /// 扁平化图表控件 - 支持折线、柱状、饼图、环形、面积图
    /// </summary>
    public class FlatChart : System.Windows.Controls.Control
    {
        static FlatChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatChart), new FrameworkPropertyMetadata(typeof(FlatChart)));
        }

        #region 数据源属性
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable<double>), typeof(FlatChart), new PropertyMetadata(null, OnDataChanged));
        public IEnumerable<double> ItemsSource { get => (IEnumerable<double>)GetValue(ItemsSourceProperty); set => SetValue(ItemsSourceProperty, value); }

        public static readonly DependencyProperty LabelsProperty = DependencyProperty.Register("Labels", typeof(IEnumerable<string>), typeof(FlatChart), new PropertyMetadata(null));
        public IEnumerable<string> Labels { get => (IEnumerable<string>)GetValue(LabelsProperty); set => SetValue(LabelsProperty, value); }
        #endregion

        #region 图表类型
        public static readonly DependencyProperty ChartTypeProperty = DependencyProperty.Register("ChartType", typeof(ChartType), typeof(FlatChart), new PropertyMetadata(ChartType.Line));
        public ChartType ChartType { get => (ChartType)GetValue(ChartTypeProperty); set => SetValue(ChartTypeProperty, value); }
        #endregion

        #region 显示属性
        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register("Stroke", typeof(SolidColorBrush), typeof(FlatChart), new PropertyMetadata(null));
        public SolidColorBrush Stroke { get => (SolidColorBrush)GetValue(StrokeProperty); set => SetValue(StrokeProperty, value); }

        public static readonly DependencyProperty FillProperty = DependencyProperty.Register("Fill", typeof(SolidColorBrush), typeof(FlatChart), new PropertyMetadata(null));
        public SolidColorBrush Fill { get => (SolidColorBrush)GetValue(FillProperty); set => SetValue(FillProperty, value); }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(FlatChart), new PropertyMetadata(string.Empty));
        public string Title { get => (string)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }

        public static readonly DependencyProperty ShowGridProperty = DependencyProperty.Register("ShowGrid", typeof(bool), typeof(FlatChart), new PropertyMetadata(true));
        public bool ShowGrid { get => (bool)GetValue(ShowGridProperty); set => SetValue(ShowGridProperty, value); }

        public static readonly DependencyProperty StartAngleProperty = DependencyProperty.Register("StartAngle", typeof(double), typeof(FlatChart), new PropertyMetadata(-90.0));
        public double StartAngle { get => (double)GetValue(StartAngleProperty); set => SetValue(StartAngleProperty, value); }
        #endregion

        private static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { if (d is FlatChart chart) chart.InvalidateVisual(); }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            double width = ActualWidth, height = ActualHeight;
            if (width < 20 || height < 20) return;

            var data = ItemsSource?.ToList() ?? new List<double>();
            if (!data.Any()) return;

            switch (ChartType)
            {
                case ChartType.Line:
                case ChartType.Area:
                    RenderLineOrAreaChart(drawingContext, width, height, data);
                    break;
                case ChartType.Bar:
                    RenderBarChart(drawingContext, width, height, data);
                    break;
                case ChartType.Pie:
                    RenderPieChart(drawingContext, width, height, data);
                    break;
                case ChartType.Donut:
                    RenderDonutChart(drawingContext, width, height, data);
                    break;
            }
        }

        private void RenderLineOrAreaChart(DrawingContext dc, double w, double h, List<double> data)
        {
            double pad = 30, cw = w - pad * 2, ch = h - pad * 2;
            double max = data.Max() == 0 ? 1 : data.Max();
            double stepX = data.Count > 1 ? cw / (data.Count - 1) : 0;

            if (ShowGrid)
            {
                var gridPen = new WpfPen(new SolidColorBrush(WpfColor.FromRgb(240, 240, 240)), 1);
                for (int i = 0; i <= 5; i++)
                {
                    double y = pad + (ch / 5) * i;
                    dc.DrawLine(gridPen, new WpfPoint(pad, y), new WpfPoint(w - pad, y));
                }
            }

            var axisPen = new WpfPen(new SolidColorBrush(WpfColor.FromRgb(200, 200, 200)), 1.5);
            dc.DrawLine(axisPen, new WpfPoint(pad, h - pad), new WpfPoint(w - pad, h - pad));
            dc.DrawLine(axisPen, new WpfPoint(pad, pad), new WpfPoint(pad, h - pad));

            var brush = Stroke ?? (WpfApplication.Current?.FindResource("PrimaryBrush") as SolidColorBrush) ?? new SolidColorBrush(WpfColor.FromRgb(64, 158, 255));

            if (ChartType == ChartType.Area)
            {
                var geo = new StreamGeometry();
                using (var ctx = geo.Open())
                {
                    ctx.BeginFigure(new System.Windows.Point(pad, h - pad), false, false);
                    ctx.LineTo(new System.Windows.Point(pad, h - pad - (data[0] / max * ch)), true, false);
                    for (int i = 1; i < data.Count; i++)
                        ctx.LineTo(new System.Windows.Point(pad + i * stepX, h - pad - (data[i] / max * ch)), true, false);
                    ctx.LineTo(new System.Windows.Point(w - pad, h - pad), true, false);
                    ctx.Close();
                }
                dc.DrawGeometry(Fill ?? new SolidColorBrush(WpfColor.FromArgb(50, 64, 158, 255)), null, geo);
            }

            var lineGeo = new StreamGeometry();
            using (var ctx = lineGeo.Open())
            {
                ctx.BeginFigure(new WpfPoint(pad, h - pad - (data[0] / max * ch)), false, false);
                for (int i = 1; i < data.Count; i++)
                    ctx.LineTo(new WpfPoint(pad + i * stepX, h - pad - (data[i] / max * ch)), true, true);
            }
            dc.DrawGeometry(null, new WpfPen(brush, 2), lineGeo);

            for (int i = 0; i < data.Count; i++)
            {
                double x = pad + i * stepX, y = h - pad - (data[i] / max * ch);
                dc.DrawEllipse(brush, null, new WpfPoint(x, y), 4, 4);
            }
        }

        private void RenderBarChart(DrawingContext dc, double w, double h, List<double> data)
        {
            double pad = 30, cw = w - pad * 2, ch = h - pad * 2;
            double max = data.Max() == 0 ? 1 : data.Max();
            double barW = (cw / data.Count) * 0.6, spacing = cw / data.Count;

            if (ShowGrid)
            {
                var gridPen = new WpfPen(new SolidColorBrush(WpfColor.FromRgb(240, 240, 240)), 1);
                for (int i = 0; i <= 5; i++)
                {
                    double y = pad + (ch / 5) * i;
                    dc.DrawLine(gridPen, new WpfPoint(pad, y), new WpfPoint(w - pad, y));
                }
            }

            var axisPen = new WpfPen(new SolidColorBrush(WpfColor.FromRgb(200, 200, 200)), 1.5);
            dc.DrawLine(axisPen, new WpfPoint(pad, h - pad), new WpfPoint(w - pad, h - pad));
            dc.DrawLine(axisPen, new WpfPoint(pad, pad), new WpfPoint(pad, h - pad));

            var brush = Stroke ?? (WpfApplication.Current?.FindResource("PrimaryBrush") as SolidColorBrush) ?? new SolidColorBrush(WpfColor.FromRgb(64, 158, 255));
            for (int i = 0; i < data.Count; i++)
            {
                double barH = (data[i] / max) * ch;
                double x = pad + (i * spacing) + (spacing - barW) / 2, y = h - pad - barH;
                dc.DrawRectangle(brush, null, new WpfRect(x, y, barW, barH));
            }
        }

        private void RenderPieChart(DrawingContext dc, double w, double h, List<double> data)
        {
            double total = data.Sum(), cx = w / 2, cy = h / 2, r = Math.Min(w, h) / 2 - 10;
            double startAngle = StartAngle;
            var colors = GetChartColors();

            for (int i = 0; i < data.Count; i++)
            {
                double sweepAngle = (data[i] / total) * 360;
                var geo = CreateArcGeometry(cx, cy, r, r, startAngle, sweepAngle);
                dc.DrawGeometry(i < colors.Count ? colors[i] : colors[0], new WpfPen(WpfBrushes.White, 2), geo);
                startAngle += sweepAngle;
            }
        }

        private void RenderDonutChart(DrawingContext dc, double w, double h, List<double> data)
        {
            double total = data.Sum(), cx = w / 2, cy = h / 2;
            double outerR = Math.Min(w, h) / 2 - 10, innerR = outerR * 0.6;
            double startAngle = StartAngle;
            var colors = GetChartColors();

            for (int i = 0; i < data.Count; i++)
            {
                double sweepAngle = (data[i] / total) * 360;
                var geo = CreateDonutGeometry(cx, cy, outerR, innerR, startAngle, sweepAngle);
                dc.DrawGeometry(i < colors.Count ? colors[i] : colors[0], null, geo);
                startAngle += sweepAngle;
            }
        }

        private StreamGeometry CreateArcGeometry(double cx, double cy, double rx, double ry, double startAngle, double sweepAngle)
        {
            var geo = new StreamGeometry();
            using (var ctx = geo.Open())
            {
                ctx.BeginFigure(new WpfPoint(cx, cy), false, false);
                double startRad = startAngle * Math.PI / 180, endRad = (startAngle + sweepAngle) * Math.PI / 180;
                ctx.LineTo(new WpfPoint(cx + rx * Math.Cos(startRad), cy + ry * Math.Sin(startRad)), true, false);
                ctx.ArcTo(new WpfPoint(cx + rx * Math.Cos(endRad), cy + ry * Math.Sin(endRad)), new WpfSize(rx, ry), 0, sweepAngle > 180, SweepDirection.Clockwise, true, false);
                ctx.LineTo(new WpfPoint(cx, cy), true, false);
                ctx.Close();
            }
            return geo;
        }

        private StreamGeometry CreateDonutGeometry(double cx, double cy, double outerR, double innerR, double startAngle, double sweepAngle)
        {
            var geo = new StreamGeometry();
            using (var ctx = geo.Open())
            {
                double startRad = startAngle * Math.PI / 180, endRad = (startAngle + sweepAngle) * Math.PI / 180;
                var outerStart = new WpfPoint(cx + outerR * Math.Cos(startRad), cy + outerR * Math.Sin(startRad));
                var outerEnd = new WpfPoint(cx + outerR * Math.Cos(endRad), cy + outerR * Math.Sin(endRad));
                var innerStart = new WpfPoint(cx + innerR * Math.Cos(startRad), cy + innerR * Math.Sin(startRad));
                var innerEnd = new WpfPoint(cx + innerR * Math.Cos(endRad), cy + innerR * Math.Sin(endRad));

                ctx.BeginFigure(outerStart, false, false);
                ctx.ArcTo(outerEnd, new WpfSize(outerR, outerR), 0, sweepAngle > 180, SweepDirection.Clockwise, true, false);
                ctx.LineTo(innerEnd, true, false);
                ctx.ArcTo(innerStart, new WpfSize(innerR, innerR), 0, sweepAngle > 180, SweepDirection.Counterclockwise, true, false);
                ctx.Close();
            }
            return geo;
        }

        private List<SolidColorBrush> GetChartColors() => new List<SolidColorBrush>
        {
            new SolidColorBrush(WpfColor.FromRgb(64, 158, 255)),
            new SolidColorBrush(WpfColor.FromRgb(101, 189, 77)),
            new SolidColorBrush(WpfColor.FromRgb(245, 166, 35)),
            new SolidColorBrush(WpfColor.FromRgb(247, 100, 96)),
            new SolidColorBrush(WpfColor.FromRgb(155, 121, 222)),
            new SolidColorBrush(WpfColor.FromRgb(25, 190, 208)),
            new SolidColorBrush(WpfColor.FromRgb(250, 135, 0)),
            new SolidColorBrush(WpfColor.FromRgb(234, 106, 227))
        };
    }

    public enum ChartType { Line, Bar, Pie, Donut, Area }
}
