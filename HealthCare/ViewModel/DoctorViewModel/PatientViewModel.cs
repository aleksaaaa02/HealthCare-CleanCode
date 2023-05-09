using HealthCare.Model;
using HealthCare.ViewModel;

namespace HealthCare.ViewModels.DoctorViewModel
{
    public class PatientViewModel : ViewModelBase
    {
        private Patient _patient;
        public string JMBG => _patient.JMBG;
        public string NameAndLastName => _patient.Name + " " + _patient.LastName;

        public string Birthday => _patient.BirthDate.ToString("d");

        public string Gender => _patient.Gender.ToString();

        public PatientViewModel(Patient patient) 
        {
            _patient = patient;
        }
    }
}
