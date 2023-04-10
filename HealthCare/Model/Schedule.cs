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

        public static List<Appointment> GetDoctorAppointmentsForDays(Doctor doctor, DateTime start, int days)
        {
            List<Appointment> appointments = new List<Appointment>();
            DateTime end = start.AddDays(days);
            foreach(Appointment appointment in Appointments)
            {
                if(appointment.TimeSlot.InBetweenDates(start, end))
                {
                    appointments.Add(appointment);
                }
            }
            return appointments;

        }
        public static List<Appointment> GetPatientAppointments(Patient Patient)
        {
            List<Appointment> PatientAppointments = new List<Appointment>();
            foreach (Appointment appointment in Appointments)
            {
                if (appointment.Patient == Patient)
                {
                    PatientAppointments.Add(appointment);
                }
            }
            return PatientAppointments;
        }
 
        public static void CreateAppointment(Appointment appointment)
        {
            if(appointment.Patient.IsAvailable(appointment.TimeSlot) && appointment.Doctor.IsAvailable(appointment.TimeSlot))
            {
                Appointments.Add(appointment);
            }
        }

        public  static void UpdateAppointment(Appointment updatedAppointment)
        {
            Appointment? appointment = Appointments.Find(x => x.AppointmentID == updatedAppointment.AppointmentID);
            int appointmentIndex = Appointments.IndexOf(appointment);
            if (appointmentIndex != -1) {
                Appointments.RemoveAt(appointmentIndex);
                if (updatedAppointment.Patient.IsAvailable(updatedAppointment.TimeSlot) && updatedAppointment.Doctor.IsAvailable(updatedAppointment.TimeSlot))
                {
                    Appointments.Insert(appointmentIndex,updatedAppointment);
                }
                else
                {
                    Appointments.Insert(appointmentIndex, appointment);
                }

            }
        }

        public static void DeleteAppointment(int appointmentID)
        {
            Appointment? appointment = Appointments.Find(x => x.AppointmentID == appointmentID);
            if (appointment != null) Appointments.Remove(appointment);
        }

        public static Appointment GetAppointment(int appointmentID)
        {
            return Appointments.Find(x => x.AppointmentID == appointmentID);
        }   
    }
}
