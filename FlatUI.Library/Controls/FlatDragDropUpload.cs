using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DragDropEffects = System.Windows.DragDropEffects;
using IDataObject = System.Windows.IDataObject;
using DragEventArgs = System.Windows.DragEventArgs;
using Brush = System.Windows.Media.Brush;
using DataFormats = System.Windows.DataFormats;

namespace FlatUI.Library.Controls
{
    /// <summary>
    /// 拖拽上传控件
    /// </summary>
    public class FlatDragDropUpload : ContentControl
    {
        static FlatDragDropUpload()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatDragDropUpload), new FrameworkPropertyMetadata(typeof(FlatDragDropUpload)));
        }

        public FlatDragDropUpload()
        {
            AllowDrop = true;
            
            // 注册拖拽事件
            DragEnter += OnDragEnter;
            DragLeave += OnDragLeave;
            DragOver += OnDragOver;
            Drop += OnDrop;
        }

        #region 属性

        /// <summary>
        /// 允许的文件类型
        /// </summary>
        public static readonly DependencyProperty AllowedFileTypesProperty =
            DependencyProperty.Register("AllowedFileTypes", typeof(string), typeof(FlatDragDropUpload), 
                new FrameworkPropertyMetadata("*.*", FrameworkPropertyMetadataOptions.AffectsRender));

        public string AllowedFileTypes
        {
            get => (string)GetValue(AllowedFileTypesProperty);
            set => SetValue(AllowedFileTypesProperty, value);
        }

        /// <summary>
        /// 最大文件大小（字节）
        /// </summary>
        public static readonly DependencyProperty MaxFileSizeProperty =
            DependencyProperty.Register("MaxFileSize", typeof(long), typeof(FlatDragDropUpload), 
                new FrameworkPropertyMetadata(10L * 1024 * 1024, FrameworkPropertyMetadataOptions.AffectsRender)); // 10MB

        public long MaxFileSize
        {
            get => (long)GetValue(MaxFileSizeProperty);
            set => SetValue(MaxFileSizeProperty, value);
        }

        /// <summary>
        /// 是否允许多文件上传
        /// </summary>
        public static readonly DependencyProperty AllowMultipleFilesProperty =
            DependencyProperty.Register("AllowMultipleFiles", typeof(bool), typeof(FlatDragDropUpload), 
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

        public bool AllowMultipleFiles
        {
            get => (bool)GetValue(AllowMultipleFilesProperty);
            set => SetValue(AllowMultipleFilesProperty, value);
        }

        /// <summary>
        /// 拖拽区域背景色
        /// </summary>
        public static readonly DependencyProperty DragAreaBackgroundProperty =
            DependencyProperty.Register("DragAreaBackground", typeof(Brush), typeof(FlatDragDropUpload), 
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public Brush DragAreaBackground
        {
            get => (Brush)GetValue(DragAreaBackgroundProperty);
            set => SetValue(DragAreaBackgroundProperty, value);
        }

        /// <summary>
        /// 拖拽区域边框色
        /// </summary>
        public static readonly DependencyProperty DragAreaBorderBrushProperty =
            DependencyProperty.Register("DragAreaBorderBrush", typeof(Brush), typeof(FlatDragDropUpload), 
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public Brush DragAreaBorderBrush
        {
            get => (Brush)GetValue(DragAreaBorderBrushProperty);
            set => SetValue(DragAreaBorderBrushProperty, value);
        }

        /// <summary>
        /// 拖拽区域边框厚度
        /// </summary>
        public static readonly DependencyProperty DragAreaBorderThicknessProperty =
            DependencyProperty.Register("DragAreaBorderThickness", typeof(Thickness), typeof(FlatDragDropUpload), 
                new FrameworkPropertyMetadata(new Thickness(2), FrameworkPropertyMetadataOptions.AffectsRender));

        public Thickness DragAreaBorderThickness
        {
            get => (Thickness)GetValue(DragAreaBorderThicknessProperty);
            set => SetValue(DragAreaBorderThicknessProperty, value);
        }

        /// <summary>
        /// 拖拽区域圆角半径
        /// </summary>
        public static readonly DependencyProperty DragAreaCornerRadiusProperty =
            DependencyProperty.Register("DragAreaCornerRadius", typeof(CornerRadius), typeof(FlatDragDropUpload), 
                new FrameworkPropertyMetadata(new CornerRadius(8), FrameworkPropertyMetadataOptions.AffectsRender));

        public CornerRadius DragAreaCornerRadius
        {
            get => (CornerRadius)GetValue(DragAreaCornerRadiusProperty);
            set => SetValue(DragAreaCornerRadiusProperty, value);
        }

        /// <summary>
        /// 拖拽提示文本
        /// </summary>
        public static readonly DependencyProperty DragHintTextProperty =
            DependencyProperty.Register("DragHintText", typeof(string), typeof(FlatDragDropUpload), 
                new FrameworkPropertyMetadata("拖拽文件到此处上传", FrameworkPropertyMetadataOptions.AffectsRender));

        public string DragHintText
        {
            get => (string)GetValue(DragHintTextProperty);
            set => SetValue(DragHintTextProperty, value);
        }

        /// <summary>
        /// 拖拽中提示文本
        /// </summary>
        public static readonly DependencyProperty DragOverHintTextProperty =
            DependencyProperty.Register("DragOverHintText", typeof(string), typeof(FlatDragDropUpload), 
                new FrameworkPropertyMetadata("释放文件以上传", FrameworkPropertyMetadataOptions.AffectsRender));

        public string DragOverHintText
        {
            get => (string)GetValue(DragOverHintTextProperty);
            set => SetValue(DragOverHintTextProperty, value);
        }

        /// <summary>
        /// 是否显示拖拽区域
        /// </summary>
        public static readonly DependencyProperty ShowDragAreaProperty =
            DependencyProperty.Register("ShowDragArea", typeof(bool), typeof(FlatDragDropUpload), 
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));

        public bool ShowDragArea
        {
            get => (bool)GetValue(ShowDragAreaProperty);
            set => SetValue(ShowDragAreaProperty, value);
        }

        /// <summary>
        /// 是否正在拖拽
        /// </summary>
        public static readonly DependencyProperty IsDragOverProperty =
            DependencyProperty.Register("IsDragOver", typeof(bool), typeof(FlatDragDropUpload), 
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

        public bool IsDragOver
        {
            get => (bool)GetValue(IsDragOverProperty);
            set => SetValue(IsDragOverProperty, value);
        }

        #endregion

        #region 事件

        /// <summary>
        /// 文件上传事件
        /// </summary>
        public static readonly RoutedEvent FilesUploadedEvent =
            EventManager.RegisterRoutedEvent("FilesUploaded", RoutingStrategy.Bubble, 
                typeof(EventHandler<FilesUploadedEventArgs>), typeof(FlatDragDropUpload));

        public event EventHandler<FilesUploadedEventArgs> FilesUploaded
        {
            add => AddHandler(FilesUploadedEvent, value);
            remove => RemoveHandler(FilesUploadedEvent, value);
        }

        /// <summary>
        /// 文件验证失败事件
        /// </summary>
        public static readonly RoutedEvent FileValidationFailedEvent =
            EventManager.RegisterRoutedEvent("FileValidationFailed", RoutingStrategy.Bubble, 
                typeof(EventHandler<FileValidationFailedEventArgs>), typeof(FlatDragDropUpload));

        public event EventHandler<FileValidationFailedEventArgs> FileValidationFailed
        {
            add => AddHandler(FileValidationFailedEvent, value);
            remove => RemoveHandler(FileValidationFailedEvent, value);
        }

        #endregion

        #region 事件处理

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            if (IsValidDragData(e.Data))
            {
                IsDragOver = true;
                e.Effects = DragDropEffects.Copy;
                e.Handled = true;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void OnDragLeave(object sender, DragEventArgs e)
        {
            IsDragOver = false;
            e.Handled = true;
        }

        private void OnDragOver(object sender, DragEventArgs e)
        {
            if (IsValidDragData(e.Data))
            {
                IsDragOver = true;
                e.Effects = DragDropEffects.Copy;
                e.Handled = true;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            IsDragOver = false;

            if (!IsValidDragData(e.Data))
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
                return;
            }

            var files = GetFilesFromData(e.Data);
            if (files.Count == 0)
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
                return;
            }

            // 验证文件
            var validFiles = ValidateFiles(files);
            
            if (validFiles.Count > 0)
            {
                // 触发文件上传事件
                var args = new FilesUploadedEventArgs(FilesUploadedEvent, this, validFiles);
                RaiseEvent(args);
                
                e.Effects = DragDropEffects.Copy;
                e.Handled = true;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        #endregion

        #region 辅助方法

        private bool IsValidDragData(IDataObject data)
        {
            return data.GetDataPresent(DataFormats.FileDrop);
        }

        private List<string> GetFilesFromData(IDataObject data)
        {
            var files = new List<string>();
            
            if (data.GetDataPresent(DataFormats.FileDrop))
            {
                var filePaths = data.GetData(DataFormats.FileDrop) as string[];
                if (filePaths != null)
                {
                    files.AddRange(filePaths);
                }
            }
            
            return files;
        }

        private List<string> ValidateFiles(List<string> files)
        {
            var validFiles = new List<string>();
            var failedFiles = new List<FileValidationInfo>();

            // 检查文件数量
            if (!AllowMultipleFiles && files.Count > 1)
            {
                foreach (var file in files)
                {
                    failedFiles.Add(new FileValidationInfo
                    {
                        FilePath = file,
                        ErrorType = FileValidationErrorType.MultipleFilesNotAllowed
                    });
                }
                
                RaiseFileValidationFailedEvent(failedFiles);
                return validFiles;
            }

            foreach (var file in files)
            {
                var validationResult = ValidateFile(file);
                if (validationResult.IsValid)
                {
                    validFiles.Add(file);
                }
                else
                {
                    failedFiles.Add(validationResult);
                }
            }

            if (failedFiles.Count > 0)
            {
                RaiseFileValidationFailedEvent(failedFiles);
            }

            return validFiles;
        }

        private FileValidationInfo ValidateFile(string filePath)
        {
            var info = new FileValidationInfo { FilePath = filePath };

            // 检查文件是否存在
            if (!File.Exists(filePath))
            {
                info.ErrorType = FileValidationErrorType.FileNotFound;
                return info;
            }

            // 检查文件类型
            if (!IsFileTypeAllowed(filePath))
            {
                info.ErrorType = FileValidationErrorType.FileTypeNotAllowed;
                return info;
            }

            // 检查文件大小
            var fileInfo = new FileInfo(filePath);
            if (fileInfo.Length > MaxFileSize)
            {
                info.ErrorType = FileValidationErrorType.FileTooLarge;
                info.FileSize = fileInfo.Length;
                return info;
            }

            info.IsValid = true;
            return info;
        }

        private bool IsFileTypeAllowed(string filePath)
        {
            if (AllowedFileTypes == "*.*") return true;

            var fileExtension = Path.GetExtension(filePath).ToLower();
            var allowedTypes = AllowedFileTypes.ToLower().Split(';');

            foreach (var type in allowedTypes)
            {
                var pattern = type.Trim();
                if (pattern.StartsWith("*.") && pattern.Length > 2)
                {
                    var ext = pattern.Substring(1); // 去掉 *
                    if (fileExtension == ext) return true;
                }
                else if (pattern == fileExtension)
                {
                    return true;
                }
            }

            return false;
        }

        private void RaiseFileValidationFailedEvent(List<FileValidationInfo> failedFiles)
        {
            var args = new FileValidationFailedEventArgs(FileValidationFailedEvent, this, failedFiles);
            RaiseEvent(args);
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 手动选择文件
        /// </summary>
        public void SelectFiles()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = $"允许的文件类型 ({AllowedFileTypes})|{AllowedFileTypes}",
                Multiselect = AllowMultipleFiles
            };

            if (dialog.ShowDialog() == true)
            {
                var validFiles = ValidateFiles(new List<string>(dialog.FileNames));
                if (validFiles.Count > 0)
                {
                    var args = new FilesUploadedEventArgs(FilesUploadedEvent, this, validFiles);
                    RaiseEvent(args);
                }
            }
        }

        /// <summary>
        /// 清空拖拽状态
        /// </summary>
        public void ClearDragState()
        {
            IsDragOver = false;
        }

        #endregion
    }

    /// <summary>
    /// 文件上传事件参数
    /// </summary>
    public class FilesUploadedEventArgs : RoutedEventArgs
    {
        public List<string> Files { get; }

        public FilesUploadedEventArgs(RoutedEvent routedEvent, object source, List<string> files) 
            : base(routedEvent, source)
        {
            Files = files ?? new List<string>();
        }
    }

    /// <summary>
    /// 文件验证失败事件参数
    /// </summary>
    public class FileValidationFailedEventArgs : RoutedEventArgs
    {
        public List<FileValidationInfo> FailedFiles { get; }

        public FileValidationFailedEventArgs(RoutedEvent routedEvent, object source, List<FileValidationInfo> failedFiles) 
            : base(routedEvent, source)
        {
            FailedFiles = failedFiles ?? new List<FileValidationInfo>();
        }
    }

    /// <summary>
    /// 文件验证信息
    /// </summary>
    public class FileValidationInfo
    {
        public string FilePath { get; set; } = string.Empty;
        public bool IsValid { get; set; }
        public FileValidationErrorType ErrorType { get; set; }
        public long FileSize { get; set; }
    }

    /// <summary>
    /// 文件验证错误类型
    /// </summary>
    public enum FileValidationErrorType
    {
        None,
        FileNotFound,
        FileTypeNotAllowed,
        FileTooLarge,
        MultipleFilesNotAllowed
    }
}