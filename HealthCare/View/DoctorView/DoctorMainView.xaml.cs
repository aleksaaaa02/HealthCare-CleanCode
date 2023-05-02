using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModels;
using HealthCare.ViewModels.DoctorViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace HealthCare.View.DoctorView
{
    /// <summary>
    /// Interaction logic for DoctorMainView.xaml
    /// </summary>
    public partial class DoctorMainView : Window
    {
        private Window _loginWindow;

        public DoctorMainView(Window loginWindow, Hospital hospital)
        {
            _loginWindow = loginWindow;
            InitializeComponent();
            // Treba Hospital Model
            DataContext = new DoctorMainViewModel(hospital);
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            _loginWindow.Show();
            Close();
        }

        /*
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Application.Current.Shutdown();
        }
        */
    }
}
