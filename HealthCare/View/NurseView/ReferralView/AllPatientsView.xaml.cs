using HealthCare.Application;
using HealthCare.Model;
using System.Windows;
using HealthCare.ViewModel.NurseViewModel;
using System.Windows.Controls;
using HealthCare.View.NurseView.PrescriptionView;

namespace HealthCare.View.NurseView.ReferralView
{
    public partial class AllPatientsView : Window
    {
        private PatientViewModel _model;
        private Patient? _patient;
        public AllPatientsView()
        {
            InitializeComponent();

            _model = new PatientViewModel();
            DataContext = _model;
            _model.Update();

            _patient = null;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void lvPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _patient = (Patient)lvPatients.SelectedItem;
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
                return;

            new PatientsReferralsView(_patient).ShowDialog();
        }

        private void btnPrescribe_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
                return;

            new PatientsPrescriptionsView(_patient).ShowDialog();

        }

        private bool Validate()
        {
            if (_patient is null)
            {
                Utility.ShowWarning("Izaberite pacijenta.");
                return false;
            }
            return true;
        }

    }
}
