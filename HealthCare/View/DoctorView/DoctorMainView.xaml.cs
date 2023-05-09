using HealthCare.Context;
using HealthCare.ViewModels.DoctorViewModel;
using System.Windows;

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
