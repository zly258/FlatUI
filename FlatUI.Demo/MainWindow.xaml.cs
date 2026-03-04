using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Collections.ObjectModel;
 using FlatUI.Library.Controls;

 namespace FlatUI.Demo;

 public partial class MainWindow : FlatWindow
    {
     public ObservableCollection<DemoItem> Items { get; set; }
     public ObservableCollection<PropertyItem> PropertyList { get; set; }
     public ObservableCollection<CardItem> CardItems { get; set; }
     public List<double> ChartData { get; set; } = new List<double> { 10, 25, 15, 40, 20, 35, 30 };

     public MainWindow()
     {
         InitializeComponent();
         
         Items = new ObservableCollection<DemoItem>
         {
             new DemoItem { Id = 1, Name = "Sensor Alpha", Status = "Online", Value = 45.2 },
             new DemoItem { Id = 2, Name = "Sensor Beta", Status = "Offline", Value = 0.0 },
             new DemoItem { Id = 3, Name = "Sensor Gamma", Status = "Online", Value = 12.8 },
             new DemoItem { Id = 4, Name = "Sensor Delta", Status = "Warning", Value = 88.5 },
         };

         PropertyList = new ObservableCollection<PropertyItem>
         {
             new PropertyItem { Category = "Device", Name = "Model", Value = "S-2000" },
             new PropertyItem { Category = "Device", Name = "Firmware", Value = "1.2.4" },
             new PropertyItem { Category = "Network", Name = "IP Address", Value = "192.168.1.50" },
             new PropertyItem { Category = "Network", Name = "Port", Value = 8080 },
         };

         CardItems = new ObservableCollection<CardItem>
         {
             new CardItem { Title = "Storage A", Subtitle = "Local SSD", Description = "Capacity: 1TB, Used: 450GB", Progress = 45 },
             new CardItem { Title = "Storage B", Subtitle = "NAS Storage", Description = "Capacity: 10TB, Used: 8.2TB", Progress = 82 },
             new CardItem { Title = "Cloud Cache", Subtitle = "Azure Cache", Description = "Capacity: 500GB, Used: 120GB", Progress = 24 },
         };
         
         DataContext = this;
         Loaded += (s, e) => {
             NotificationService.RegisterHost(this);
             TrayManager.Initialize("FlatUI Demo", System.Drawing.SystemIcons.Application);
              
              var menu = new System.Windows.Forms.ContextMenuStrip();
              menu.Items.Add("Show MainWindow", null, (s, e) => {
                  this.Show();
                  this.WindowState = WindowState.Normal;
                  this.Activate();
              });
              menu.Items.Add("Exit", null, (s, e) => Application.Current.Shutdown());
              TrayManager.SetContextMenu(menu);
          };
         
         Closed += (s, e) => TrayManager.Dispose();
      }

      private void TestDialog_Click(object sender, RoutedEventArgs e)
      {
          FlatMessageBox.Show("This is a custom flat message box for industrial software applications.", "System Notice", this);
      }

      private void Notify_Click(object sender, RoutedEventArgs e)
      {
          var types = new[] { StatusType.Info, StatusType.Success, StatusType.Warning, StatusType.Error };
          var random = new Random();
          var type = types[random.Next(types.Length)];
          NotificationService.Show($"Event of type '{type}'", "This is a notification message that will disappear automatically.", type);
      }

      private void OpenDrawer_Click(object sender, RoutedEventArgs e) => SideDrawer.IsOpen = true;
      private void CloseDrawer_Click(object sender, RoutedEventArgs e) => SideDrawer.IsOpen = false;

      private void SetLightMode_Click(object sender, RoutedEventArgs e) => ThemeManager.ChangeTheme(ThemeMode.Light);
      private void SetDarkMode_Click(object sender, RoutedEventArgs e) => ThemeManager.ChangeTheme(ThemeMode.Dark);

      private void Screenshot_Click(object sender, RoutedEventArgs e)
      {
          var bmp = ScreenshotService.CaptureScreen();
          ScreenshotImage.Source = bmp;
          NotificationService.Show("Screenshot Captured", "Current screen has been captured and displayed below.", StatusType.Success);
      }

      private void Crop_Click(object sender, RoutedEventArgs e)
      {
          var cropped = Cropper.GetCroppedImage();
          if (cropped != null)
          {
              ScreenshotImage.Source = cropped;
              NotificationService.Show("Image Cropped", "Selected area has been cropped.", StatusType.Success);
          }
      }

      private void OpenFloatingWindow_Click(object sender, RoutedEventArgs e)
      {
          var fw = new FloatingWindow
          {
              IconPathData = "M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm-1 14.5v-9l6 4.5-6 4.5z",
              Content = new TextBlock { Text = "42", Foreground = (Brush)FindResource("WhiteTextBrush"), FontSize = 20, FontWeight = FontWeights.Bold, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center }
          };
          fw.Show();
      }

      private void FlashTray_Click(object sender, RoutedEventArgs e)
      {
          TrayManager.StartFlashing();
          NotificationService.Show("Tray Flashing", "The tray icon is now flashing.", StatusType.Info);
      }

      private void StopFlash_Click(object sender, RoutedEventArgs e)
      {
          TrayManager.StopFlashing();
          NotificationService.Show("Flash Stopped", "The tray icon stopped flashing.", StatusType.Success);
      }

      private void ShowSystemNotify_Click(object sender, RoutedEventArgs e)
      {
          NotificationService.ShowSystem("FlatUI System Notify", "This is a native Windows balloon tip notification!");
      }
  }

public class DemoItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public double Value { get; set; }
}

public class CardItem
{
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Progress { get; set; }
}