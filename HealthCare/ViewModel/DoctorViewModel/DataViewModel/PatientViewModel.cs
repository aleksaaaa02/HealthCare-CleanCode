using HealthCare.Model;
using HealthCare.View;

namespace HealthCare.ViewModel.DoctorViewModel.DataViewModel
{
    public class PatientViewModel : ViewModelBase
    {
        private Patient _patient;
        public string JMBG => _patient.JMBG;
        public string NameAndLastName => _patient.Name + " " + _patient.LastName;
        public string Birthday => ViewUtil.ToString(_patient.BirthDate);
        public string Gender => ViewUtil.Translate(_patient.Gender);

        public PatientViewModel(Patient patient)
        {
            _patient = patient;
        }
    }
}
