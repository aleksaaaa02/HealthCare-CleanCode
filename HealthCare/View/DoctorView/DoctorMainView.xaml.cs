using HealthCare.Context;
using HealthCare.ViewModels.DoctorViewModel;
using System.ComponentModel;
using System.Windows;

namespace HealthCare.View.DoctorView
{
    /// <summary>
    /// Interaction logic for DoctorMainView.xaml
    /// </summary>
    public partial class DoctorMainView : Window
    {
        private MainWindow _loginWindow;

        public DoctorMainView(MainWindow loginWindow, Hospital hospital)
        {
            _loginWindow = loginWindow;
            InitializeComponent();
            DataContext = new DoctorMainViewModel(hospital, loginWindow, this);
            
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {            
            _loginWindow.Show();
        }
    }
}
