using HealthCare.Command;
using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;
using HealthCare.View.DoctorView;
using HealthCare.ViewModel.DoctorViewModel.Examination;
using HealthCare.ViewModels.DoctorViewModel;

namespace HealthCare.ViewModel.DoctorViewModel.PatientInformation.Commands
{
    public class ShowPatientInfoCommand : CommandBase
    {
        private readonly PatientService _patientService;
        private readonly ViewModelBase _viewModel;
        private readonly bool _isEdit;

        public ShowPatientInfoCommand(ViewModelBase view, bool isEdit)
        {
            _patientService = Injector.GetService<PatientService>();
            _viewModel = view;
            _isEdit = isEdit;
        }

        public override void Execute(object parameter)
        {
            Patient? patient = ExtractPatient();
            if (patient is null) { return; }

            new PatientInformationView(patient, _isEdit).ShowDialog();
            UpdateViewModel();
        }

        private Patient? ExtractPatient()
        {
            if (_viewModel is DoctorMainViewModel doctorMainViewModel)
            {
                var appointment = doctorMainViewModel.SelectedAppointment;
                if (appointment is null)
                {
                    Utility.ShowWarning("Morate odabrati pregled/operaciju iz tabele!");
                    return null;
                }
                return _patientService.GetAccount(appointment.JMBG);
            }

            if (_viewModel is PatientSearchViewModel patientSearchViewModel)
            {
                var selectedPatient = patientSearchViewModel.SelectedPatient;
                if (selectedPatient is null)
                {
                    Utility.ShowWarning("Morate odabrati pacijenta iz tabele!");
                    return null;
                }
                return _patientService.GetAccount(selectedPatient.JMBG);
            }

            if (_viewModel is DoctorExamViewModel doctorExamViewModel)
            {
                var selectedPatient = doctorExamViewModel.SelectedPatient;
                return selectedPatient;
            }
            return null;
        }

        private void UpdateViewModel()
        {
            if (_viewModel is DoctorExamViewModel doctorExamViewModel)
            {
                doctorExamViewModel.RefreshView();
            }
        }
    }
}
