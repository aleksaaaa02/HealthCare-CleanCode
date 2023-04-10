using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using Microsoft.Win32;

namespace HealthCare
{
    public class Patient : User
    {
        public bool Blocked { get; set; }
        public MedicalRecord? MedicalRecord { get; set; }

        public Patient(string name, string lastName, string jMBG, DateTime birthDate, string phoneNumber, string address, string userName, string password, Gender gender, bool blocked, MedicalRecord? medicalRecord) : base(name, lastName, jMBG, birthDate, phoneNumber, address, userName,password,gender)
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

        public new string[] toCSV()
        {
            string[] userValues = base.toCSV();
            string[] patientValues = {Blocked.ToString()};
            string[] medicalRecordValues = {""};
            if (MedicalRecord != null)
                medicalRecordValues = MedicalRecord.toCSV();
            string[] csvValues = userValues.Concat(patientValues).Concat(medicalRecordValues).ToArray();
            return csvValues;
        }

        public new void fromCSV(string[] values)
        {
            Name = values[0];
            LastName = values[1];
            JMBG = values[2];
            BirthDate = DateTime.Parse(values[3]);
            PhoneNumber = values[4];
            Address = values[5];
            UserName = values[6];
            Password = values[7];
            Gender = (Gender)Enum.Parse(typeof(Gender), values[8]);

            Blocked = bool.Parse(values[8]);
            MedicalRecord = new MedicalRecord();
            if (values[9] != "")
                MedicalRecord.fromCSV(values[9].Split("\\|"));
        }

    }
}
