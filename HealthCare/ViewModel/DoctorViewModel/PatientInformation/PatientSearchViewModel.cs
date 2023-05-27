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
        private readonly DoctorService _doctorService;
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
            _doctorService = Injector.GetService<DoctorService>();
            _patients = new ObservableCollection<PatientViewModel>();
            ShowEditPatientCommand = new ShowPatientInfoCommand(this, true);
            Update();
        }

        public void Update()
        {
            _patients.Clear();
            foreach (var patient in _doctorService.GetExaminedPatients((Doctor)Context.Current))
            {
                _patients.Add(new PatientViewModel(patient));

            }
        }

    }
}
