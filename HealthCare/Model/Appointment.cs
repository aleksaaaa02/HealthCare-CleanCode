using HealthCare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Repository;

namespace HealthCare.Model
{
    public class Appointment : ISerializable
    {
        public int AppointmentID {get;set;}
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public bool IsOperation { get; set; }
        public int AnamnesisID { get; set; }
        public bool IsUrgent { get; set; }

        public Appointment() : this(new Patient(), new Doctor(), new TimeSlot(), false)
        {
            
        }

        public Appointment(Patient patient, Doctor doctor, TimeSlot timeSlot, bool isOperation)
        {
            Patient = patient;
            Doctor = doctor;
            TimeSlot = timeSlot;
            IsOperation = isOperation;
            AnamnesisID = 0;
            IsUrgent = false; 
        }

        public Appointment(Patient patient, Doctor doctor, TimeSlot timeSlot, bool isOperation, int anamnesisID)
        {
            Patient = patient;
            Doctor = doctor;
            TimeSlot = timeSlot;
            IsOperation = isOperation;
            AnamnesisID = anamnesisID;
            IsUrgent = false;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {AppointmentID.ToString(), Patient.JMBG.ToString(), Doctor.JMBG.ToString(), TimeSlot.ToString(), IsOperation.ToString(),AnamnesisID.ToString(),IsUrgent.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            AppointmentID = int.Parse(values[0]);
            Patient = new Patient();
            Patient.JMBG = values[1];
            
            Doctor = new Doctor();
            Doctor.JMBG = values[2];

            TimeSlot = TimeSlot.Parse(values[3]);
            IsOperation = bool.Parse(values[4]);
            AnamnesisID = int.Parse(values[5]);
            IsUrgent = Convert.ToBoolean(values[6]);
        }
    }
}
