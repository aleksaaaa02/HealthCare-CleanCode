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
            DataContext = new DoctorMainViewModel(hospital);
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            _loginWindow.Show();
            this.Hide();
        }

        
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            _loginWindow.ExitApp();
        }
        
    }
}
