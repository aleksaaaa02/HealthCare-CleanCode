using HealthCare.Context;
using System;
using System.ComponentModel;
using System.Windows;

namespace HealthCare.View.ManagerView
{
    public partial class ManagerMenu : Window
    {
        private MainWindow _loginWindow;

        public ManagerMenu(MainWindow loginWindow)
        {
            InitializeComponent();
            _loginWindow = loginWindow;
        }

        private void Button_Equipment(object sender, EventArgs e)
        {
            new InventoryListingView().ShowDialog();
        }
        private void Button_Ordering(object sender, RoutedEventArgs e)
        {
            new EquipmentOrderView().ShowDialog();
        }

        private void Button_Rearranging(object sender, RoutedEventArgs e)
        {
            new EquipmentRearrangingView().ShowDialog();
        }

        private void Button_Logout(object sender, RoutedEventArgs e)
        {
            Close();
            _loginWindow.Show();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _loginWindow.Show();
        }
    }
}
