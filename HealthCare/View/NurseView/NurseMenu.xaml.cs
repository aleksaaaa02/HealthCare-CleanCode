using HealthCare.Application;
using HealthCare.View.NurseView.OrderMedicationView;
using HealthCare.View.NurseView.ReferralView;
using HealthCare.View.ReceptionView;
using HealthCare.View.UrgentAppointmentView;
using System.ComponentModel;
using System.Windows;

namespace HealthCare.View.NurseView
{
    public partial class NurseMenu : Window
    {
        private MainWindow _window;
        public NurseMenu(MainWindow window)
        {
            InitializeComponent();
            _window = window;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Close();
            _window.Show();
        }

        private void mnuCRUD_Click(object sender, RoutedEventArgs e)
        {
            new NurseMainView().ShowDialog();
        }

        private void mnuReception_Click(object sender, RoutedEventArgs e)
        {
            new MainReceptionView().ShowDialog();
        }

        private void mnuUrgent_Click(object sender, RoutedEventArgs e)
        {
            new UrgentView().ShowDialog();
        }

        private void mnuReferral_Click(object sender, RoutedEventArgs e)
        {
            new AllPatientsView().ShowDialog();
        }

        private void mnuOrder_Click(object sender, RoutedEventArgs e)
        {
            new OrderMedicationView().ShowDialog();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _window.Show();
        }
    }
}
