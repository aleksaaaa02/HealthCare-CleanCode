using HealthCare.Service;
using HealthCare.ViewModel.InventoryItemModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HealthCare.View.InventoryView
{
    /// <summary>
    /// Interaction logic for InventoryMainView.xaml
    /// </summary>
    public partial class InventoryMainView : Window
    {
        public InventoryMainView()
        {
            InitializeComponent();
            Inventory inv = new Inventory("../../../Resources/inventory_items.csv");

            
            DataContext = new InventoryListingViewModel(inv);
            Show();
        }
    }
}
