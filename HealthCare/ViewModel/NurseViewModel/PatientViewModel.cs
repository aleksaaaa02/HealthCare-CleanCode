using System.Collections.ObjectModel;
using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;

namespace HealthCare.ViewModel.NurseViewModel
{
    public class PatientViewModel
    {
        private PatientService _patientService;

        public PatientViewModel()
        {
            Patients = new ObservableCollection<Patient>();
            _patientService = Injector.GetService<PatientService>();
        }

        public ObservableCollection<Patient> Patients { get; set; }

        public void Update()
        {
            Patients.Clear();
            foreach (var patient in _patientService.GetAll())
                Patients.Add(patient);
        }
    }
}