using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.ViewModels.DoctorViewModel
{
    public class PatientViewModel : BaseViewModel
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
