using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using Panel = System.Windows.Controls.Panel;
using Application = System.Windows.Application;
using Orientation = System.Windows.Controls.Orientation;
using HorizontalAlignment = System.Windows.HorizontalAlignment;
using TranslateTransform = System.Windows.Media.TranslateTransform;

namespace FlatUI.Library.Controls
{
    /// <summary>
    /// Toast通知服务 - 用于管理Toast的显示和隐藏
    /// </summary>
    public static class ToastService
    {
        private static Panel? _toastContainer;
        private static readonly List<FlatToast> _activeToasts = new List<FlatToast>();

        /// <summary>
        /// 初始化Toast服务
        /// </summary>
        /// <param name="container">Toast容器（通常是Grid或Canvas）</param>
        public static void Initialize(Panel container)
        {
            _toastContainer = container;
        }

        /// <summary>
        /// 显示信息Toast
        /// </summary>
        public static void ShowInfo(string message, int duration = 3000)
        {
            Show(message, ToastType.Info, duration);
        }

        /// <summary>
        /// 显示成功Toast
        /// </summary>
        public static void ShowSuccess(string message, int duration = 3000)
        {
            Show(message, ToastType.Success, duration);
        }

        /// <summary>
        /// 显示警告Toast
        /// </summary>
        public static void ShowWarning(string message, int duration = 5000)
        {
            Show(message, ToastType.Warning, duration);
        }

        /// <summary>
        /// 显示错误Toast
        /// </summary>
        public static void ShowError(string message, int duration = 5000)
        {
            Show(message, ToastType.Error, duration);
        }

        /// <summary>
        /// 显示Toast
        /// </summary>
        public static void Show(string message, ToastType type = ToastType.Info, int duration = 3000)
        {
            if (_toastContainer == null)
            {
                throw new InvalidOperationException("Toast服务未初始化，请先调用Initialize方法");
            }

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                var toast = new FlatToast
                {
                    Content = message,
                    Type = type,
                    Duration = duration
                };

                toast.Closed += OnToastClosed;
                
                _toastContainer.Children.Add(toast);
                _activeToasts.Add(toast);
                
                // 设置Toast位置
                UpdateToastPositions();
                
                // 显示动画
                ShowToastAnimation(toast);
                
                // 自动隐藏
                if (duration > 0)
                {
                    _ = AutoHideToast(toast, duration);
                }
            });
        }

        private static async Task AutoHideToast(FlatToast toast, int duration)
        {
            await Task.Delay(duration);
            
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (_activeToasts.Contains(toast))
                {
                    HideToastAnimation(toast);
                }
            });
        }

        private static void ShowToastAnimation(FlatToast toast)
        {
            var animation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            
            toast.BeginAnimation(UIElement.OpacityProperty, animation);
            
            var translateAnimation = new DoubleAnimation
            {
                From = -20,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            
            var transform = new TranslateTransform();
            toast.RenderTransform = transform;
            transform.BeginAnimation(TranslateTransform.YProperty, translateAnimation);
        }

        private static void HideToastAnimation(FlatToast toast)
        {
            var animation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };
            
            animation.Completed += (s, e) =>
            {
                if (_toastContainer != null && _toastContainer.Children.Contains(toast))
                {
                    _toastContainer.Children.Remove(toast);
                    _activeToasts.Remove(toast);
                    UpdateToastPositions();
                }
            };
            
            toast.BeginAnimation(UIElement.OpacityProperty, animation);
            
            var translateAnimation = new DoubleAnimation
            {
                From = 0,
                To = -20,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };
            
            var transform = toast.RenderTransform as TranslateTransform;
            transform?.BeginAnimation(TranslateTransform.YProperty, translateAnimation);
        }

        private static void OnToastClosed(object sender, RoutedEventArgs e)
        {
            if (sender is FlatToast toast)
            {
                HideToastAnimation(toast);
            }
        }

        private static void UpdateToastPositions()
        {
            if (_toastContainer is Canvas canvas)
            {
                double top = 20;
                foreach (var toast in _activeToasts)
                {
                    Canvas.SetTop(toast, top);
                    Canvas.SetLeft(toast, (canvas.ActualWidth - toast.ActualWidth) / 2);
                    top += toast.ActualHeight + 10;
                }
            }
            else if (_toastContainer is Grid grid)
            {
                // 对于Grid，使用StackPanel布局
                var stackPanel = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 20, 0, 0)
                };
                
                grid.Children.Clear();
                grid.Children.Add(stackPanel);
                
                foreach (var toast in _activeToasts)
                {
                    stackPanel.Children.Add(toast);
                }
            }
        }

        /// <summary>
        /// 清除所有Toast
        /// </summary>
        public static void ClearAll()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var toast in _activeToasts.ToList())
                {
                    HideToastAnimation(toast);
                }
            });
        }
    }
}