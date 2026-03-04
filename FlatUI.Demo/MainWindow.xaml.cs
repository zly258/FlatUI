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

 public partial class MainWindow : Window
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
         Loaded += (s, e) => NotificationService.RegisterHost(this);
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