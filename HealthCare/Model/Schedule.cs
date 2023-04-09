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
    
        public static List<Appointment> GetDoctorAppointments(Doctor Doctor)
        { 
            return Appointments; 
        }
        public static List <Appointment> GetPatientAppointments(Patient Patinet) 
        {
            return Appointments;
        }
 
        public static void CreateAppointment(Appointment appointment)
        {
            if(appointment.Patient.IsAvailable() && appointment.Doctor.IsAvailable())
            {
                Appointments.Add(appointment);
            }
        }

        public  static void UpdateAppointment(Appointment updatedAppointment)
        {
            Appointment appointment = Appointments.Find(x => x.AppointmentID == updatedAppointment.AppointmentID);
            int appointmentIndex = Appointments.IndexOf(appointment);
            if (appointmentIndex == -1) Appointments[appointmentIndex] = updatedAppointment;
        }

        public static void DeleteAppointment(int appointmentID)
        {
            Appointment appointment = Appointments.Find(x => x.AppointmentID == appointmentID);
            if (appointment != null) Appointments.Remove(appointment);
        }

        public static Appointment GetAppointment(int appointmentID)
        {
            return Appointments.Find(x => x.AppointmentID == appointmentID);
        }
        

        //iterate through appointments and check if the parameter appointment overlaps with any of them
        public static bool IsAvailable(Appointment appointment)
        {
            foreach (Appointment a in Appointments)
            {
                if (a.DoctorID == appointment.DoctorID && a.PatientID == appointment.PatientID)
                {
                    if (a.StartTime < appointment.EndTime && a.EndTime > appointment.StartTime)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


          
    }
}
