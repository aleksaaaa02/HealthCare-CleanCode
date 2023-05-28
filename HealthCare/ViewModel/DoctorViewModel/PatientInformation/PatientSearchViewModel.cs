using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.ViewModel.DoctorViewModel.DataViewModel;
using HealthCare.ViewModel.DoctorViewModel.PatientInformation.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HealthCare.ViewModel.DoctorViewModel.PatientInformation
{
    public class PatientSearchViewModel : ViewModelBase
    {
        private readonly PatientService _patientService;
        private readonly AppointmentService _appointmentService;

        private ObservableCollection<PatientViewModel> _patients;
        public IEnumerable<PatientViewModel> Patients => _patients;

        private PatientViewModel _selectedPatient;
        public PatientViewModel SelectedPatient
        {
            get => _selectedPatient;
            set
            {
                _selectedPatient = value;
                OnPropertyChanged(nameof(SelectedPatient));
            }
        }

        public ICommand ShowEditPatientCommand { get; }

        public PatientSearchViewModel()
        {
            _appointmentService = Injector.GetService<AppointmentService>();
            _patientService = Injector.GetService<PatientService>();
            _patients = new ObservableCollection<PatientViewModel>();
            ShowEditPatientCommand = new ShowPatientInfoCommand(this, true);
            Update();
        }

        public void Update()
        {
            _patients.Clear();
            foreach (var patientJMBG in _appointmentService.GetExaminedPatients(Context.Current.JMBG))
            {
                Patient patient = _patientService.Get(patientJMBG);
                _patients.Add(new PatientViewModel(patient));
            }
        }

    }
}
