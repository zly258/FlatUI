using System.Windows;
using System.Windows.Controls;

namespace FlatUI.Library.Controls
{
    /// <summary>
    /// 进度条控件 - 支持多种样式和类型
    /// </summary>
    public class ProgressBar : System.Windows.Controls.Control
    {
        static ProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressBar), new FrameworkPropertyMetadata(typeof(ProgressBar)));
        }

        #region 属性

        /// <summary>
        /// 当前值
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(ProgressBar), 
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        /// <summary>
        /// 最大值
        /// </summary>
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(ProgressBar), 
                new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(ProgressBar), 
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        /// <summary>
        /// 进度条类型
        /// </summary>
        public static readonly DependencyProperty ProgressTypeProperty =
            DependencyProperty.Register("ProgressType", typeof(ProgressType), typeof(ProgressBar), 
                new FrameworkPropertyMetadata(ProgressType.Linear, FrameworkPropertyMetadataOptions.AffectsRender));

        public ProgressType ProgressType
        {
            get => (ProgressType)GetValue(ProgressTypeProperty);
            set => SetValue(ProgressTypeProperty, value);
        }

        /// <summary>
        /// 是否显示百分比文本
        /// </summary>
        public static readonly DependencyProperty ShowPercentageProperty =
            DependencyProperty.Register("ShowPercentage", typeof(bool), typeof(ProgressBar), 
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));

        public bool ShowPercentage
        {
            get => (bool)GetValue(ShowPercentageProperty);
            set => SetValue(ShowPercentageProperty, value);
        }

        /// <summary>
        /// 是否显示动画效果
        /// </summary>
        public static readonly DependencyProperty IsAnimatedProperty =
            DependencyProperty.Register("IsAnimated", typeof(bool), typeof(ProgressBar), 
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));

        public bool IsAnimated
        {
            get => (bool)GetValue(IsAnimatedProperty);
            set => SetValue(IsAnimatedProperty, value);
        }

        /// <summary>
        /// 进度条颜色
        /// </summary>
        public static readonly DependencyProperty ProgressBrushProperty =
            DependencyProperty.Register("ProgressBrush", typeof(System.Windows.Media.Brush), typeof(ProgressBar), 
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public System.Windows.Media.Brush ProgressBrush
        {
            get => (System.Windows.Media.Brush)GetValue(ProgressBrushProperty);
            set => SetValue(ProgressBrushProperty, value);
        }

        /// <summary>
        /// 进度条高度
        /// </summary>
        public static readonly DependencyProperty ProgressHeightProperty =
            DependencyProperty.Register("ProgressHeight", typeof(double), typeof(ProgressBar), 
                new FrameworkPropertyMetadata(8.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double ProgressHeight
        {
            get => (double)GetValue(ProgressHeightProperty);
            set => SetValue(ProgressHeightProperty, value);
        }

        /// <summary>
        /// 进度条圆角半径
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ProgressBar), 
                new FrameworkPropertyMetadata(new CornerRadius(4), FrameworkPropertyMetadataOptions.AffectsRender));

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        /// 进度条状态
        /// </summary>
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(ProgressStatus), typeof(ProgressBar), 
                new FrameworkPropertyMetadata(ProgressStatus.Normal, FrameworkPropertyMetadataOptions.AffectsRender));

        public ProgressStatus Status
        {
            get => (ProgressStatus)GetValue(StatusProperty);
            set => SetValue(StatusProperty, value);
        }

        /// <summary>
        /// 是否显示条纹动画
        /// </summary>
        public static readonly DependencyProperty IsStripedProperty =
            DependencyProperty.Register("IsStriped", typeof(bool), typeof(ProgressBar), 
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

        public bool IsStriped
        {
            get => (bool)GetValue(IsStripedProperty);
            set => SetValue(IsStripedProperty, value);
        }

        #endregion

        #region 事件

        /// <summary>
        /// 进度完成事件
        /// </summary>
        public static readonly RoutedEvent ProgressCompletedEvent =
            EventManager.RegisterRoutedEvent("ProgressCompleted", RoutingStrategy.Bubble, 
                typeof(RoutedEventHandler), typeof(ProgressBar));

        public event RoutedEventHandler ProgressCompleted
        {
            add => AddHandler(ProgressCompletedEvent, value);
            remove => RemoveHandler(ProgressCompletedEvent, value);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 设置进度值
        /// </summary>
        public void SetValue(double value)
        {
            Value = value;
            
            // 检查是否完成
            if (value >= Maximum)
            {
                RaiseEvent(new RoutedEventArgs(ProgressCompletedEvent, this));
            }
        }

        /// <summary>
        /// 重置进度
        /// </summary>
        public void Reset()
        {
            Value = Minimum;
        }

        /// <summary>
        /// 获取进度百分比
        /// </summary>
        public double GetPercentage()
        {
            if (Maximum <= Minimum) return 0;
            return (Value - Minimum) / (Maximum - Minimum) * 100;
        }

        #endregion
    }

    /// <summary>
    /// 进度条类型
    /// </summary>
    public enum ProgressType
    {
        /// <summary>
        /// 线性进度条
        /// </summary>
        Linear,
        
        /// <summary>
        /// 环形进度条
        /// </summary>
        Circular,
        
        /// <summary>
        /// 半环形进度条
        /// </summary>
        SemiCircular,
        
        /// <summary>
        /// 波浪形进度条
        /// </summary>
        Wave
    }

    /// <summary>
    /// 进度条状态
    /// </summary>
    public enum ProgressStatus
    {
        /// <summary>
        /// 正常状态
        /// </summary>
        Normal,
        
        /// <summary>
        /// 成功状态
        /// </summary>
        Success,
        
        /// <summary>
        /// 警告状态
        /// </summary>
        Warning,
        
        /// <summary>
        /// 错误状态
        /// </summary>
        Error,
        
        /// <summary>
        /// 信息状态
        /// </summary>
        Info
    }
}