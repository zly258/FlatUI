using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Collections;
using System;

namespace FlatUI.Library.Controls
{
    public static class DragDropHelper
    {
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(DragDropHelper), new PropertyMetadata(false, OnIsEnabledChanged));

        public static void SetIsEnabled(DependencyObject obj, bool value) => obj.SetValue(IsEnabledProperty, value);
        public static bool GetIsEnabled(DependencyObject obj) => (bool)obj.GetValue(IsEnabledProperty);

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is System.Windows.Controls.ListBox listBox)
            {
                if ((bool)e.NewValue)
                {
                    listBox.PreviewMouseLeftButtonDown += ListBox_PreviewMouseLeftButtonDown;
                    listBox.PreviewMouseMove += ListBox_PreviewMouseMove;
                    listBox.Drop += ListBox_Drop;
                    listBox.AllowDrop = true;
                }
                else
                {
                    listBox.PreviewMouseLeftButtonDown -= ListBox_PreviewMouseLeftButtonDown;
                    listBox.PreviewMouseMove -= ListBox_PreviewMouseMove;
                    listBox.Drop -= ListBox_Drop;
                    listBox.AllowDrop = false;
                }
            }
        }

        private static System.Windows.Point _dragStartPoint;

        private static void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragStartPoint = e.GetPosition(null);
        }

        private static void ListBox_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var mousePos = e.GetPosition(null);
                var diff = _dragStartPoint - mousePos;

                if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    var listBox = sender as System.Windows.Controls.ListBox;
                    var item = GetItemUnderMouse(listBox, mousePos);
                    if (item != null)
                    {
                        DragDrop.DoDragDrop(listBox!, item, System.Windows.DragDropEffects.Move);
                    }
                }
            }
        }

        private static void ListBox_Drop(object sender, System.Windows.DragEventArgs e)
        {
            var listBox = sender as System.Windows.Controls.ListBox;
            var formats = e.Data.GetFormats();
            if (formats.Length == 0) return;
            var data = e.Data.GetData(formats[0]);
            var sourceList = listBox?.ItemsSource as IList;

            if (sourceList != null && data != null)
            {
                int index = GetDropIndex(listBox!, e.GetPosition(listBox));
                if (index >= 0)
                {
                    sourceList.Remove(data);
                    if (index > sourceList.Count) index = sourceList.Count;
                    sourceList.Insert(index, data);
                }
            }
        }

        private static object? GetItemUnderMouse(System.Windows.Controls.ListBox? listBox, System.Windows.Point pos)
        {
            if (listBox == null) return null;
            var hitTestResult = VisualTreeHelper.HitTest(listBox, pos);
            var element = hitTestResult?.VisualHit;
            while (element != null)
            {
                if (element is ListBoxItem item) return item.DataContext;
                element = VisualTreeHelper.GetParent(element);
            }
            return null;
        }

        private static int GetDropIndex(System.Windows.Controls.ListBox listBox, System.Windows.Point pos)
        {
            for (int i = 0; i < listBox.Items.Count; i++)
            {
                var item = listBox.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                if (item != null)
                {
                    var itemPos = item.TranslatePoint(new System.Windows.Point(0, 0), listBox);
                    if (pos.Y < itemPos.Y + item.ActualHeight / 2) return i;
                }
            }
            return listBox.Items.Count;
        }
    }
}