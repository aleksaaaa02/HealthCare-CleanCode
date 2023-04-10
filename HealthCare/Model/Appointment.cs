using HealthCare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Serializer;

namespace HealthCare.Model
{
    public class Appointment : ISerializable
    {
        public int AppointmentID {get;set;}
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public bool IsOperation { get; set; }

        public Appointment()
        {
            
        }

        public Appointment(Patient patient, Doctor doctor, TimeSlot timeSlot, bool isOperation)
        {
            Patient = patient;
            Doctor = doctor;
            TimeSlot = timeSlot;
            IsOperation = isOperation;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {AppointmentID.ToString(), Patient.JMBG.ToString(), Doctor.JMBG.ToString(), TimeSlot.ToString(), IsOperation.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            AppointmentID = Convert.ToInt32(values[0]);
            Patient = new Patient();
            Patient.JMBG = values[1];
            
            Doctor = new Doctor();
            Doctor.JMBG = values[2];

            string date = values[3].Split(' ')[0];
            string timeSpan = values[3].Split(' ')[1];
            TimeSlot = new TimeSlot(DateTime.Parse(date), TimeSpan.Parse(timeSpan));
            IsOperation = Convert.ToBoolean(values[4]);
        }
    }
}
