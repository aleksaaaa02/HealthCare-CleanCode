using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.ViewModels.DoctorViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthCare.ViewModel.DoctorViewModel
{
    public class PatientSearchViewModel : ViewModelBase
    {
        private Hospital _hospital;
        private ObservableCollection<PatientViewModel> patients;

        private PatientViewModel _selectedPatient;
        public PatientViewModel SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                OnPropertyChanged(nameof(SelectedPatient));
            }
        }

        public IEnumerable<PatientViewModel> Patients => patients;
        public ICommand ShowEditPatientCommand { get; }

        public PatientSearchViewModel(Hospital hospital)
        {
            _hospital = hospital;
            patients = new ObservableCollection<PatientViewModel>();
            ShowEditPatientCommand = new ShowPatientInfoCommand(hospital, this);
            Update();
        }

        public void Update()
        {
            patients.Clear();
            foreach (var patient in _hospital.DoctorService.GetExaminedPatients((Doctor)_hospital.Current))
            {
                patients.Add(new PatientViewModel(patient));
                
            }
        }

    }
}
