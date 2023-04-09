using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    internal class Doctor : User
    {
        public string Specialization { get; set; }
        public Doctor(string name, string lastName, string jMBG, DateTime birthDate, string phoneNumber, string address, string userName, string password, string specialization) : base(name, lastName, jMBG, birthDate, phoneNumber, address, userName, password)
        {
            Specialization = specialization;

        }


        public bool IsAvailable(TimeSlot term)
        {
            List<Appointment> DoctorAppointments = Schedule.GetDoctorAppointments(this);
            foreach(Appointment appointment in DoctorAppointments) 
            {
                if (appointment.TimeSlot.Overlaps(term))
                {
                    return false;
                }
            }
            return true;
        }
        public bool IsCapable(string NeededSpecialization)
        {
            if (Specialization == NeededSpecialization)
            {
                return true;
            }
            return false;
        }
    }
}
