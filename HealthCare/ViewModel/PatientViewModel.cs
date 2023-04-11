using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.ViewModel
{
    public class PatientViewModel
    {
        public ObservableCollection<Patient> Patients { get; set; }

        public PatientViewModel()
        {
            Patients = new ObservableCollection<Patient>();
        }
    }
}
