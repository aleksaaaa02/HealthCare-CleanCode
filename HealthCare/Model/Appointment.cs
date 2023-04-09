using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HealthCare.Model
{
    public class Appointment
    {
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public bool IsOperation { get; set; }

        public Appointment(Patient patient, Doctor doctor, TimeSlot timeSlot, bool isOperation)
        {
            Patient = patient;
            Doctor = doctor;
            TimeSlot = timeSlot;
            IsOperation = isOperation;
        }

    }
}
