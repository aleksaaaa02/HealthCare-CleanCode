using HealthCare.Context;
using HealthCare.Model;
using HealthCare.View.DoctorView;
using System.Linq;
using System.Windows;

namespace HealthCare.Command
{
    public class SavePatientChangesCommand : CommandBase
    {
        private readonly PatientInforamtionViewModel _viewModel;
        private readonly Hospital _hospital;
        private readonly Patient _selectedPatient;
        public SavePatientChangesCommand(Hospital hospital, Patient patient, PatientInforamtionViewModel viewModel) {
            _hospital = hospital;
            _viewModel = viewModel;
            _selectedPatient = patient;
        }
        public override void Execute(object parameter)
        {
            UpdatePatientMedicalHistory();
            ShowSuccesMessage();
        }

        private void UpdatePatientMedicalHistory()
        {
            string[] previousDisease = _viewModel.PreviousDisease.ToArray();
            _hospital.PatientService.UpdatePatientMedicalHistory(_selectedPatient, previousDisease);
        }

        private void ShowSuccesMessage()
        {
            MessageBox.Show("Pacijent uspesno sacuvan!", "Obavestenje", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
