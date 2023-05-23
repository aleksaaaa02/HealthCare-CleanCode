using HealthCare.Context;
using HealthCare.Model;
using System.Windows;
using HealthCare.ViewModel.NurseViewModel;
using System.Windows.Controls;

namespace HealthCare.View.NurseView.ReferralView
{
    public partial class AllPatientsView : Window
    {
        private Hospital _hospital;
        private PatientViewModel _model;
        private Patient? _patient;
        public AllPatientsView(Hospital hospital)
        {
            InitializeComponent();
            
            _hospital = hospital;

            _model = new PatientViewModel(_hospital.PatientService);
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
            if(_patient is null)
            {
                Utility.ShowWarning("Izaberite pacijenta.");
                return;
            }

            new PatientsReferralsView(_patient,_hospital).ShowDialog();
        }
    }
}
