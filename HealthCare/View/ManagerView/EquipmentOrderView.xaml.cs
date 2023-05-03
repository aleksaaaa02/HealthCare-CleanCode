using HealthCare.Context;
using HealthCare.ViewModel.ManagerViewModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for EquipmentOrderView.xaml
    /// </summary>
    public partial class EquipmentOrderView : Window
    {
        private EquipmentOrderViewModel _model;
        private Window _loginWindow;
        public EquipmentOrderView(Window loginWindow, Hospital hospital)
        {
            InitializeComponent();
            _loginWindow = loginWindow;
            _model = new EquipmentOrderViewModel(hospital);
            DataContext = _model;
        }

        private void Button_Reset(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Order(object sender, RoutedEventArgs e)
        {

        }
    }
}
