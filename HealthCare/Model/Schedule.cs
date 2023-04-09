using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    public static class Schedule
    {
        public static List<Appointment> Appointments = new();
    
        public static List<Appointment> GetDoctorAppointments(Doctor Doctor)
        { 
            List<Appointment> DoctorAppointments = new List<Appointment>();
            foreach(Appointment appointment in Appointments)
            {
                if(appointment.Doctor == Doctor)
                {
                    DoctorAppointments.Add(appointment);
                }
            }
            return DoctorAppointments; 
        }
        public static List<Appointment> GetPatientAppointments(Patient Patinet)
        {
            return Appointments;
        }
    
    }
}
