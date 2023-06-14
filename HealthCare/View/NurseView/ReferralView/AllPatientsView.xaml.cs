using System.Windows;
using System.Windows.Controls;
using HealthCare.Model;
using HealthCare.View.NurseView.PrescriptionView;
using HealthCare.ViewModel.NurseViewModel;

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

        private void btnShowPatientsReferral_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
                return;

            new PatientsReferralsView(_patient).ShowDialog();
        }

        private void btnShowTreatmantReferral_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
                return;

            new TreatmantReferralsView(_patient).ShowDialog();
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
                ViewUtil.ShowWarning("Izaberite pacijenta.");
                return false;
            }

            return true;
        }
    }
}