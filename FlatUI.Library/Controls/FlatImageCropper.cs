using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FlatUI.Library.Controls
{
    /// <summary>
    /// 图片裁剪控件
    /// </summary>
    public class FlatImageCropper : System.Windows.Controls.Control
    {
        private Canvas? _canvas;
        private System.Windows.Shapes.Rectangle? _selectionRect;
        private System.Windows.Controls.Image? _image;
        private System.Windows.Point _startPoint;

        static FlatImageCropper()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatImageCropper), new FrameworkPropertyMetadata(typeof(FlatImageCropper)));
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ImageSource), typeof(FlatImageCropper), new PropertyMetadata(null));

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _canvas = GetTemplateChild("PART_Canvas") as Canvas;
            _image = GetTemplateChild("PART_Image") as System.Windows.Controls.Image;
            _selectionRect = GetTemplateChild("PART_SelectionRect") as System.Windows.Shapes.Rectangle;

            if (_canvas != null)
            {
                _canvas.MouseDown += Canvas_MouseDown;
                _canvas.MouseMove += Canvas_MouseMove;
                _canvas.MouseUp += Canvas_MouseUp;
            }
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_selectionRect == null || _canvas == null) return;
            
            _startPoint = e.GetPosition(_canvas);
            _selectionRect.Visibility = Visibility.Visible;
            Canvas.SetLeft(_selectionRect, _startPoint.X);
            Canvas.SetTop(_selectionRect, _startPoint.Y);
            _selectionRect.Width = 0;
            _selectionRect.Height = 0;
            _canvas.CaptureMouse();
        }

        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (_selectionRect == null || _canvas == null || !_canvas.IsMouseCaptured) return;

            var currentPoint = e.GetPosition(_canvas);
            double x = Math.Min(_startPoint.X, currentPoint.X);
            double y = Math.Min(_startPoint.Y, currentPoint.Y);
            double width = Math.Abs(_startPoint.X - currentPoint.X);
            double height = Math.Abs(_startPoint.Y - currentPoint.Y);

            Canvas.SetLeft(_selectionRect, x);
            Canvas.SetTop(_selectionRect, y);
            _selectionRect.Width = width;
            _selectionRect.Height = height;
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_canvas != null && _canvas.IsMouseCaptured)
            {
                _canvas.ReleaseMouseCapture();
            }
        }

        public CroppedBitmap? GetCroppedImage()
        {
            if (_selectionRect == null || _image == null || Source is not BitmapSource bitmapSource) return null;

            double scaleX = bitmapSource.PixelWidth / _image.ActualWidth;
            double scaleY = bitmapSource.PixelHeight / _image.ActualHeight;

            int x = (int)(Canvas.GetLeft(_selectionRect) * scaleX);
            int y = (int)(Canvas.GetTop(_selectionRect) * scaleY);
            int width = (int)(_selectionRect.Width * scaleX);
            int height = (int)(_selectionRect.Height * scaleY);

            if (width <= 0 || height <= 0) return null;

            return new CroppedBitmap(bitmapSource, new Int32Rect(x, y, width, height));
        }
    }
}