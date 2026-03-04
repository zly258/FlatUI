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
         
         DataContext = this;
     }
 }

public class DemoItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public double Value { get; set; }
}