using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    static public class Schedule
    {
        public static List<Appointment> Appointments = new();
    
        public static List<Appointment> GetDoctorAppointments(Doctor Doctor)
        { 
            return Appointments; 
        }
        public static List <Appointment> GetPatientAppointments(Patient Patinet) 
        {
            return Appointments;
        }
    
    }
}
