using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HealthCare.Model;

namespace HealthCare.Model
{
    public class Patient : User
    {
        public bool Blocked { get; set; }
        public MedicalRecord? MedicalRecord { get; set; }

        public Patient(string name, string lastName, string jMBG, DateTime birthDate, string phoneNumber, string address, string userName, string password,bool blocked, MedicalRecord? medicalRecord) : base(name, lastName, jMBG, birthDate, phoneNumber, address, userName,password)
        {
            Blocked = blocked;
            MedicalRecord = medicalRecord;
        }
        public bool IsAvailable(TimeSlot term)
        {
            List<Appointment> PatientAppointments = Schedule.GetPatientAppointments(this);
            foreach (Appointment appointment in PatientAppointments)
            {
                if (appointment.TimeSlot.Overlaps(term))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
