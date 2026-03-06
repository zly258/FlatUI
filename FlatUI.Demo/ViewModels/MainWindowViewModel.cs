using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FlatUI.Library.Controls;

namespace FlatUI.Demo.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Models.NavigationItem> _navigationItems = null!;
        private Models.NavigationItem? _selectedItem;
        private object _currentContent = null!;
        private string _searchText = null!;

        public ObservableCollection<Models.NavigationItem> NavigationItems
        {
            get => _navigationItems;
            set
            {
                _navigationItems = value;
                OnPropertyChanged();
            }
        }

        public Models.NavigationItem? SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                LoadContent(value);
            }
        }

        public object CurrentContent
        {
            get => _currentContent;
            set
            {
                _currentContent = value;
                OnPropertyChanged();
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterNavigation(value);
            }
        }

        public ICommand ExpandCommand { get; private set; } = null!;
        public ICommand SelectCommand { get; private set; } = null!;
        public ICommand SearchCommand { get; private set; } = null!;

        public MainWindowViewModel()
        {
            NavigationItems = new ObservableCollection<Models.NavigationItem>();
            InitializeNavigation();
            ExpandCommand = new RelayCommand(ExpandItem);
            SelectCommand = new RelayCommand(SelectItem);
            SearchCommand = new RelayCommand(ExecuteSearch);
            CurrentContent = new Views.WelcomePage();
        }

        private void InitializeNavigation()
        {
            var general = new Models.NavigationItem
            {
                Title = "通用",
                Icon = "M3 13h8V3H3v10zm0 8h8v-6H3v6zm10 0h8V11h-8v10zm0-18v6h8V3h-8z",
                IsExpanded = true
            };

            general.Children.Add(new Models.NavigationItem
            {
                Title = "Button 按钮",
                ViewName = "ButtonExamples",
                Parent = general
            });

            general.Children.Add(new Models.NavigationItem
            {
                Title = "Icon 图标",
                ViewName = "IconExamples",
                Parent = general
            });

            general.Children.Add(new Models.NavigationItem
            {
                Title = "Typography 排版",
                ViewName = "TypographyExamples",
                Parent = general
            });

            general.Children.Add(new Models.NavigationItem
            {
                Title = "Avatar 头像",
                ViewName = "AvatarExamples",
                Parent = general
            });

            general.Children.Add(new Models.NavigationItem
            {
                Title = "Tag 标签",
                ViewName = "TagExamples",
                Parent = general
            });

            general.Children.Add(new Models.NavigationItem
            {
                Title = "Chart 图表",
                ViewName = "ChartExamples",
                Parent = general
            });

            var layout = new Models.NavigationItem
            {
                Title = "布局",
                Icon = "M3 13h8V3H3v10zm0 8h8v-6H3v6zm10 0h8V11h-8v10zm0-18v6h8V3h-8z"
            };

            layout.Children.Add(new Models.NavigationItem
            {
                Title = "Grid 栅格",
                ViewName = "GridExamples",
                Parent = layout
            });

            layout.Children.Add(new Models.NavigationItem
            {
                Title = "Space 间距",
                ViewName = "SpaceExamples",
                Parent = layout
            });

            layout.Children.Add(new Models.NavigationItem
            {
                Title = "Drawer 抽屉",
                ViewName = "DrawerExamples",
                Parent = layout
            });

            layout.Children.Add(new Models.NavigationItem
            {
                Title = "Pagination 分页",
                ViewName = "PaginationExamples",
                Parent = layout
            });

            layout.Children.Add(new Models.NavigationItem
            {
                Title = "TabControl 标签页",
                ViewName = "TabControlExamples",
                Parent = layout
            });

            var dataDisplay = new Models.NavigationItem
            {
                Title = "数据展示",
                Icon = "M3 13h8V3H3v10zm0 8h8v-6H3v6zm10 0h8V11h-8v10zm0-18v6h8V3h-8z"
            };

            dataDisplay.Children.Add(new Models.NavigationItem
            {
                Title = "Card 卡片",
                ViewName = "CardExamples",
                Parent = dataDisplay
            });

            dataDisplay.Children.Add(new Models.NavigationItem
            {
                Title = "List 列表",
                ViewName = "ListExamples",
                Parent = dataDisplay
            });

            dataDisplay.Children.Add(new Models.NavigationItem
            {
                Title = "Badge 徽标数",
                ViewName = "BadgeExamples",
                Parent = dataDisplay
            });

            dataDisplay.Children.Add(new Models.NavigationItem
            {
                Title = "Bubble 气泡",
                ViewName = "BubbleExamples",
                Parent = dataDisplay
            });

            var feedback = new Models.NavigationItem
            {
                Title = "反馈",
                Icon = "M3 13h8V3H3v10zm0 8h8v-6H3v6zm10 0h8V11h-8v10zm0-18v6h8V3h-8z"
            };

            feedback.Children.Add(new Models.NavigationItem
            {
                Title = "Alert 警告提示",
                ViewName = "AlertExamples",
                Parent = feedback
            });

            feedback.Children.Add(new Models.NavigationItem
            {
                Title = "Message 全局提示",
                ViewName = "MessageExamples",
                Parent = feedback
            });

            feedback.Children.Add(new Models.NavigationItem
            {
                Title = "Modal 对话框",
                ViewName = "ModalExamples",
                Parent = feedback
            });

            feedback.Children.Add(new Models.NavigationItem
            {
                Title = "Notification 通知提醒框",
                ViewName = "NotificationExamples",
                Parent = feedback
            });

            feedback.Children.Add(new Models.NavigationItem
            {
                Title = "ProgressBar 进度条",
                ViewName = "ProgressBarExamples",
                Parent = feedback
            });

            var input = new Models.NavigationItem
            {
                Title = "输入",
                Icon = "M3 13h8V3H3v10zm0 8h8v-6H3v6zm10 0h8V11h-8v10zm0-18v6h8V3h-8z"
            };

            input.Children.Add(new Models.NavigationItem
            {
                Title = "Input 输入框",
                ViewName = "InputExamples",
                Parent = input
            });

            input.Children.Add(new Models.NavigationItem
            {
                Title = "Checkbox 多选框",
                ViewName = "CheckboxExamples",
                Parent = input
            });

            input.Children.Add(new Models.NavigationItem
            {
                Title = "Radio 单选框",
                ViewName = "RadioExamples",
                Parent = input
            });

            input.Children.Add(new Models.NavigationItem
            {
                Title = "Switch 开关",
                ViewName = "SwitchExamples",
                Parent = input
            });

            input.Children.Add(new Models.NavigationItem
            {
                Title = "NumericUpDown 数字输入框",
                ViewName = "NumericUpDownExamples",
                Parent = input
            });

            var other = new Models.NavigationItem
            {
                Title = "其他",
                Icon = "M3 13h8V3H3v10zm0 8h8v-6H3v6zm10 0h8V11h-8v10zm0-18v6h8V3h-8z"
            };

            other.Children.Add(new Models.NavigationItem
            {
                Title = "Form 表单",
                ViewName = "FormExamples",
                Parent = other
            });

            other.Children.Add(new Models.NavigationItem
            {
                Title = "PropertyGrid 属性网格",
                ViewName = "PropertyGridExamples",
                Parent = other
            });

            other.Children.Add(new Models.NavigationItem
            {
                Title = "StatusBadge 状态徽章",
                ViewName = "StatusBadgeExamples",
                Parent = other
            });

            other.Children.Add(new Models.NavigationItem
            {
                Title = "Toast 消息提示",
                ViewName = "ToastExamples",
                Parent = other
            });

            other.Children.Add(new Models.NavigationItem
            {
                Title = "FloatingWindow 浮动窗口",
                ViewName = "FloatingWindowExamples",
                Parent = other
            });

            other.Children.Add(new Models.NavigationItem
            {
                Title = "DragDropUpload 拖拽上传",
                ViewName = "DragDropUploadExamples",
                Parent = other
            });

            other.Children.Add(new Models.NavigationItem
            {
                Title = "DropdownGrid 下拉网格",
                ViewName = "DropdownGridExamples",
                Parent = other
            });

            other.Children.Add(new Models.NavigationItem
            {
                Title = "DropdownTree 下拉树",
                ViewName = "DropdownTreeExamples",
                Parent = other
            });

            other.Children.Add(new Models.NavigationItem
            {
                Title = "ImageCropper 图片裁剪",
                ViewName = "ImageCropperExamples",
                Parent = other
            });

            other.Children.Add(new Models.NavigationItem
            {
                Title = "LedText LED文本",
                ViewName = "LedTextExamples",
                Parent = other
            });

            NavigationItems.Add(general);
            NavigationItems.Add(layout);
            NavigationItems.Add(dataDisplay);
            NavigationItems.Add(feedback);
            NavigationItems.Add(input);
            NavigationItems.Add(other);
        }

        private void LoadContent(Models.NavigationItem? item)
        {
            if (item == null || string.IsNullOrEmpty(item.ViewName))
                return;

            switch (item.ViewName)
            {
                case "ButtonExamples":
                    CurrentContent = new Views.ButtonExamples();
                    break;
                case "CardExamples":
                    CurrentContent = new Views.CardExamples();
                    break;
                case "IconExamples":
                    CurrentContent = new Views.IconExamples();
                    break;
                case "TypographyExamples":
                    CurrentContent = new Views.TypographyExamples();
                    break;
                case "GridExamples":
                    CurrentContent = new Views.GridExamples();
                    break;
                case "SpaceExamples":
                    CurrentContent = new Views.SpaceExamples();
                    break;
                case "ListExamples":
                    CurrentContent = new Views.ListExamples();
                    break;
                case "BadgeExamples":
                    CurrentContent = new Views.BadgeExamples();
                    break;
                case "AlertExamples":
                    CurrentContent = new Views.AlertExamples();
                    break;
                case "MessageExamples":
                    CurrentContent = new Views.MessageExamples();
                    break;
                case "ModalExamples":
                    CurrentContent = new Views.ModalExamples();
                    break;
                case "NotificationExamples":
                    CurrentContent = new Views.NotificationExamples();
                    break;
                case "InputExamples":
                    CurrentContent = new Views.InputExamples();
                    break;
                case "CheckboxExamples":
                    CurrentContent = new Views.CheckboxExamples();
                    break;
                case "RadioExamples":
                    CurrentContent = new Views.RadioExamples();
                    break;
                case "SwitchExamples":
                    CurrentContent = new Views.SwitchExamples();
                    break;
                case "AvatarExamples":
                    CurrentContent = new Views.AvatarExamples();
                    break;
                case "TagExamples":
                    CurrentContent = new Views.TagExamples();
                    break;
                case "ChartExamples":
                    CurrentContent = new Views.ChartExamples();
                    break;
                case "BubbleExamples":
                    CurrentContent = new Views.BubbleExamples();
                    break;
                case "DrawerExamples":
                    CurrentContent = new Views.DrawerExamples();
                    break;
                case "PaginationExamples":
                    CurrentContent = new Views.PaginationExamples();
                    break;
                case "ProgressBarExamples":
                    CurrentContent = new Views.ProgressBarExamples();
                    break;
                case "NumericUpDownExamples":
                    CurrentContent = new Views.NumericUpDownExamples();
                    break;
                case "TabControlExamples":
                    CurrentContent = new Views.TabControlExamples();
                    break;
                case "FormExamples":
                    CurrentContent = new Views.FormExamples();
                    break;
                case "PropertyGridExamples":
                    CurrentContent = new Views.PropertyGridExamples();
                    break;
                case "StatusBadgeExamples":
                    CurrentContent = new Views.StatusBadgeExamples();
                    break;
                case "ToastExamples":
                    CurrentContent = new Views.ToastExamples();
                    break;
                case "FloatingWindowExamples":
                    CurrentContent = new Views.FloatingWindowExamples();
                    break;
                case "DragDropUploadExamples":
                    CurrentContent = new Views.DragDropUploadExamples();
                    break;
                case "DropdownGridExamples":
                    CurrentContent = new Views.DropdownGridExamples();
                    break;
                case "DropdownTreeExamples":
                    CurrentContent = new Views.DropdownTreeExamples();
                    break;
                case "ImageCropperExamples":
                    CurrentContent = new Views.ImageCropperExamples();
                    break;
                case "LedTextExamples":
                    CurrentContent = new Views.LedTextExamples();
                    break;
                default:
                    CurrentContent = new Views.WelcomePage();
                    break;
            }
        }

        private void ExpandItem(object? parameter)
        {
            if (parameter is Models.NavigationItem item)
            {
                item.IsExpanded = !item.IsExpanded;
            }
        }

        private void SelectItem(object? parameter)
        {
            if (parameter is Models.NavigationItem item)
            {
                foreach (var category in NavigationItems)
                {
                    foreach (var child in category.Children)
                    {
                        child.IsSelected = false;
                    }
                }
                item.IsSelected = true;
                LoadContent(item);
            }
        }

        private void ExecuteSearch(object? parameter)
        {
            FilterNavigation(SearchText);
        }

        private void FilterNavigation(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                foreach (var item in NavigationItems)
                {
                    item.IsExpanded = false;
                }
                return;
            }

            foreach (var category in NavigationItems)
            {
                bool hasMatch = false;
                foreach (var child in category.Children)
                {
                    bool isMatch = child.Title.ToLower().Contains(searchText.ToLower());
                    hasMatch = hasMatch || isMatch;
                }
                category.IsExpanded = hasMatch;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
