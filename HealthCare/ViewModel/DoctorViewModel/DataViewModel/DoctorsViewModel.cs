using HealthCare.Model;

namespace HealthCare.ViewModel.DoctorViewModel.DataViewModel
{
    public class DoctorsViewModel : ViewModelBase
    {
        private readonly Doctor _doctor;
        public string JMBG => _doctor.JMBG;
        public string NameAndLastName => _doctor.Name + " " + _doctor.LastName;
        public string Specialization => _doctor.Specialization;
        

        public DoctorsViewModel(Doctor doctor) 
        { 
            _doctor = doctor;
        }
    }
}
