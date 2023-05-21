using HealthCare.Repository;
using HealthCare.Serialize;
using System;

namespace HealthCare.Model
{
    public class Appointment : RepositoryItem
    {
        public int AppointmentID { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public bool IsOperation { get; set; }
        public int AnamnesisID { get; set; }
        public bool IsUrgent { get; set; }

        public Appointment() : this(new Patient(), new Doctor(), new TimeSlot(), false) { }
        public Appointment(Patient patient, Doctor doctor, TimeSlot timeSlot, bool isOperation)
        {
            Patient = patient;
            Doctor = doctor;
            TimeSlot = timeSlot;
            IsOperation = isOperation;
            AnamnesisID = 0;
            IsUrgent = false;
        }

        public override object Key
        {
            get => AppointmentID;
            set { AppointmentID = (int)value; }
        }

        public override string[] Serialize()
        {
            return new string[] { 
                AppointmentID.ToString(), Patient.JMBG.ToString(), Doctor.JMBG.ToString(), 
                TimeSlot.ToString(), IsOperation.ToString(), AnamnesisID.ToString(), IsUrgent.ToString() };
        }

        public override void Deserialize(string[] values)
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