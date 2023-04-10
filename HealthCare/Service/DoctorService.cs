using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    public class DoctorService
    {
        public List<Doctor> Doctors;

        

        public DoctorService() 
        { 
            Doctors = new List<Doctor>();   
        }  

        public Doctor GetDoctorByJMBG(string JMBG)
        {
            return Doctors.Find(x => x.JMBG == JMBG);
        }

    }
}
