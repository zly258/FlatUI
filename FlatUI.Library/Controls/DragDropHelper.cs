using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Collections;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using DragDropEffects = System.Windows.DragDropEffects;
using DragEventArgs = System.Windows.DragEventArgs;
using ListBox = System.Windows.Controls.ListBox;
using Panel = System.Windows.Controls.Panel;
using Point = System.Windows.Point;
using DataObject = System.Windows.DataObject;
using DataFormats = System.Windows.DataFormats;
using Color = System.Windows.Media.Color;
using HorizontalAlignment = System.Windows.HorizontalAlignment;
using Size = System.Windows.Size;

namespace FlatUI.Library.Controls
{
    public static class DragDropHelper
    {
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(DragDropHelper), new PropertyMetadata(false, OnIsEnabledChanged));

        public static void SetIsEnabled(DependencyObject obj, bool value) => obj.SetValue(IsEnabledProperty, value);
        public static bool GetIsEnabled(DependencyObject obj) => (bool)obj.GetValue(IsEnabledProperty);

        // 拖拽指示器相关属性
        public static readonly DependencyProperty DragIndicatorProperty =
            DependencyProperty.RegisterAttached("DragIndicator", typeof(FrameworkElement), typeof(DragDropHelper), new PropertyMetadata(null));

        public static void SetDragIndicator(DependencyObject obj, FrameworkElement value) => obj.SetValue(DragIndicatorProperty, value);
        public static FrameworkElement GetDragIndicator(DependencyObject obj) => (FrameworkElement)obj.GetValue(DragIndicatorProperty);

        // 拖拽中样式属性
        public static readonly DependencyProperty DragOverStyleProperty =
            DependencyProperty.RegisterAttached("DragOverStyle", typeof(Style), typeof(DragDropHelper), new PropertyMetadata(null));

        public static void SetDragOverStyle(DependencyObject obj, Style value) => obj.SetValue(DragOverStyleProperty, value);
        public static Style GetDragOverStyle(DependencyObject obj) => (Style)obj.GetValue(DragOverStyleProperty);

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is System.Windows.Controls.ListBox listBox)
            {
                if ((bool)e.NewValue)
                {
                    listBox.PreviewMouseLeftButtonDown += ListBox_PreviewMouseLeftButtonDown;
                    listBox.PreviewMouseMove += ListBox_PreviewMouseMove;
                    listBox.Drop += ListBox_Drop;
                    listBox.DragOver += ListBox_DragOver;
                    listBox.DragLeave += ListBox_DragLeave;
                    listBox.AllowDrop = true;
                }
                else
                {
                    listBox.PreviewMouseLeftButtonDown -= ListBox_PreviewMouseLeftButtonDown;
                    listBox.PreviewMouseMove -= ListBox_PreviewMouseMove;
                    listBox.Drop -= ListBox_Drop;
                    listBox.DragOver -= ListBox_DragOver;
                    listBox.DragLeave -= ListBox_DragLeave;
                    listBox.AllowDrop = false;
                }
            }
        }

        private static System.Windows.Point _dragStartPoint;
        private static object? _draggedItem;
        private static ListBox? _sourceListBox;
        private static FrameworkElement? _dragIndicator;

        private static void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragStartPoint = e.GetPosition(null);
            _sourceListBox = sender as ListBox;
            
            if (_sourceListBox != null)
            {
                var item = GetItemUnderMouse(_sourceListBox, e.GetPosition(_sourceListBox));
                if (item != null)
                {
                    _draggedItem = item;
                    
                    // 创建拖拽指示器
                    CreateDragIndicator(_sourceListBox);
                }
            }
            
            if (_sourceListBox != null)
            {
                var item = GetItemUnderMouse(_sourceListBox, e.GetPosition(_sourceListBox));
                if (item != null)
                {
                    _draggedItem = item;
                    
                    // 创建拖拽指示器
                    CreateDragIndicator(_sourceListBox);
                }
            }
        }

        private static void ListBox_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && _draggedItem != null)
            {
                var mousePos = e.GetPosition(null);
                var diff = _dragStartPoint - mousePos;

                if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    // 开始拖拽
                    var dataObject = new DataObject("DragDropItem", _draggedItem);
                    
                    // 设置拖拽效果
                    var effects = DragDrop.DoDragDrop(_sourceListBox!, dataObject, DragDropEffects.Move);
                    
                    // 拖拽完成后清理
                    if (effects == DragDropEffects.None || effects == DragDropEffects.Move)
                    {
                        RemoveDragIndicator();
                    _draggedItem = null;
                    _sourceListBox = null;
                    }
                }
            }
        }

        private static void ListBox_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("DragDropItem")) return;

            var listBox = sender as ListBox;
            if (listBox == null) return;

            e.Effects = DragDropEffects.Move;
            e.Handled = true;

            // 更新拖拽指示器位置
            UpdateDragIndicator(listBox, e.GetPosition(listBox));

            // 应用拖拽中样式
            var item = GetItemUnderMouse(listBox, e.GetPosition(listBox));
            if (item != null)
            {
                var listBoxItem = GetListBoxItem(listBox, item);
                if (listBoxItem != null)
                {
                    var dragOverStyle = GetDragOverStyle(listBox);
                    if (dragOverStyle != null)
                    {
                        listBoxItem.Style = dragOverStyle;
                    }
                }
            }
        }

        private static void ListBox_DragLeave(object sender, DragEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox != null)
            {
                // 移除拖拽中样式
                ClearDragOverStyles(listBox);
            }
        }

        private static void ListBox_Drop(object sender, System.Windows.DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("DragDropItem")) return;

            var listBox = sender as ListBox;
            var draggedData = e.Data.GetData("DragDropItem");

            if (listBox != null && draggedData != null)
            {
                // 移除拖拽中样式
                ClearDragOverStyles(listBox);

                // 获取目标索引
                int targetIndex = GetDropIndex(listBox, e.GetPosition(listBox));
                
                // 处理数据移动
                MoveItemToIndex(listBox, draggedData, targetIndex);
                
                e.Effects = DragDropEffects.Move;
                e.Handled = true;
            }

            // 移除拖拽指示器
            RemoveDragIndicator();
        }

        private static void MoveItemToIndex(ListBox listBox, object draggedData, int targetIndex)
        {
            var sourceList = listBox.ItemsSource as IList;
            if (sourceList == null) return;

            // 获取源索引
            int sourceIndex = sourceList.IndexOf(draggedData);
            if (sourceIndex == -1) return;

            // 如果拖拽到同一位置，不执行操作
            if (sourceIndex == targetIndex) return;

            // 处理 ObservableCollection 的特殊情况
            if (sourceList is INotifyCollectionChanged observableCollection)
            {
                // 对于 ObservableCollection，需要先移除再插入
                sourceList.RemoveAt(sourceIndex);
                
                // 调整目标索引（因为已经移除了源项目）
                if (targetIndex > sourceIndex)
                {
                    targetIndex--;
                }
                
                if (targetIndex >= 0 && targetIndex <= sourceList.Count)
                {
                    sourceList.Insert(targetIndex, draggedData);
                }
            }
            else
            {
                // 对于普通列表，直接交换位置
                if (targetIndex >= 0 && targetIndex < sourceList.Count)
                {
                    sourceList.RemoveAt(sourceIndex);
                    sourceList.Insert(targetIndex, draggedData);
                }
            }

            // 更新选中项
            listBox.SelectedItem = draggedData;
        }

        private static void CreateDragIndicator(ListBox listBox)
        {
            RemoveDragIndicator();

            _dragIndicator = new Border
            {
                Background = new SolidColorBrush(Color.FromArgb(128, 0, 120, 215)),
                Height = 2,
                Margin = new Thickness(0, -1, 0, -1),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            var panel = GetItemsPanel(listBox);
            if (panel != null)
            {
                panel.Children.Add(_dragIndicator);
                _dragIndicator.Visibility = Visibility.Collapsed;
            }
        }

        private static void UpdateDragIndicator(ListBox listBox, Point position)
        {
            if (_dragIndicator == null) return;

            int dropIndex = GetDropIndex(listBox, position);
            
            var item = listBox.ItemContainerGenerator.ContainerFromIndex(dropIndex) as ListBoxItem;
            if (item != null)
            {
                var itemPos = item.TranslatePoint(new Point(0, 0), listBox);
                
                _dragIndicator.Margin = new Thickness(8, itemPos.Y - 1, 8, 0);
                _dragIndicator.Visibility = Visibility.Visible;
            }
            else if (dropIndex == listBox.Items.Count)
            {
                // 拖拽到最后
                var lastItem = listBox.ItemContainerGenerator.ContainerFromIndex(listBox.Items.Count - 1) as ListBoxItem;
                if (lastItem != null)
                {
                    var itemPos = lastItem.TranslatePoint(new Point(0, 0), listBox);
                    _dragIndicator.Margin = new Thickness(8, itemPos.Y + lastItem.ActualHeight - 1, 8, 0);
                    _dragIndicator.Visibility = Visibility.Visible;
                }
            }
            else
            {
                _dragIndicator.Visibility = Visibility.Collapsed;
            }
        }

        private static void RemoveDragIndicator()
        {
            if (_dragIndicator != null && _dragIndicator.Parent is Panel panel)
            {
                if (_dragIndicator != null) panel.Children.Remove(_dragIndicator);
                _dragIndicator = null;
            }
        }

        private static void ClearDragOverStyles(ListBox listBox)
        {
            foreach (var item in listBox.Items)
            {
                var listBoxItem = GetListBoxItem(listBox, item);
                if (listBoxItem != null)
                {
                    listBoxItem.ClearValue(ListBoxItem.StyleProperty);
                }
            }
        }

        private static Panel? GetItemsPanel(ListBox listBox)
        {
            var itemsPresenter = FindVisualChild<ItemsPresenter>(listBox);
            if (itemsPresenter != null)
            {
                return VisualTreeHelper.GetChild(itemsPresenter, 0) as Panel;
            }
            return null;
        }

        private static T? FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T result)
                {
                    return result;
                }
                
                var descendant = FindVisualChild<T>(child);
                if (descendant != null)
                {
                    return descendant;
                }
            }
            return null;
        }

        private static ListBoxItem? GetListBoxItem(ListBox listBox, object item)
        {
            return listBox.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;
        }

        private static object? GetItemUnderMouse(ListBox? listBox, Point pos)
        {
            if (listBox == null) return null;
            var hitTestResult = VisualTreeHelper.HitTest(listBox, pos);
            var element = hitTestResult?.VisualHit;
            while (element != null)
            {
                if (element is ListBoxItem item) return item.DataContext ?? item.Content;
                element = VisualTreeHelper.GetParent(element);
            }
            return null;
        }

        private static int GetDropIndex(ListBox listBox, Point pos)
        {
            for (int i = 0; i < listBox.Items.Count; i++)
            {
                var item = listBox.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                if (item != null)
                {
                    var itemBounds = new Rect(item.TranslatePoint(new Point(0, 0), listBox), 
                                             new Size(item.ActualWidth, item.ActualHeight));
                    
                    // 检查是否在项目上半部分
                    if (pos.Y < itemBounds.Top + itemBounds.Height * 0.5)
                    {
                        return i;
                    }
                }
            }
            return listBox.Items.Count;
        }
    }
}