using HealthCare.Context;
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

namespace HealthCare.View.ManagerView
{
    /// <summary>
    /// Interaction logic for ManagerMenu.xaml
    /// </summary>
    public partial class ManagerMenu : Window
    {
        private MainWindow _loginWindow;
        private Hospital _hospital;

        public ManagerMenu(MainWindow loginWindow, Hospital hospital)
        {
            InitializeComponent();
            _loginWindow = loginWindow;
            _hospital = hospital;
        }

        private void Button_Equipment(object sender, EventArgs e)
        {
            new InventoryListingView(this, _hospital).Show();
        }
        private void Button_Ordering(object sender, RoutedEventArgs e)
        {
            new EquipmentOrderView(this, _hospital).Show();
        }

        private void Button_Rearanging(object sender, RoutedEventArgs e)
        {
            new EquipmentRearangingView(this, _hospital).Show();
        }

        private void Button_Logout(object sender, RoutedEventArgs e)
        {
            Close();
            _loginWindow.Show();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _loginWindow.ExitApp();
        }
    }
}
