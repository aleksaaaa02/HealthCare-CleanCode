using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    static internal class Schedule
    {
        public static List<Appointment> Appointments = new();
    
        public static List<Appointment> GetDoctorAppointments()
        { 
            return Appointments; 
        }
        public static List <Appointment> GetPatientAppointments() 
        {
            return Appointments;
        }
    
    }
}
