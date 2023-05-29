using HealthCare.Application.Common;
using HealthCare.Serialize;
using HealthCare.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Model
{
    public class Patient : User
    {
        public bool Blocked { get; set; }
        public MedicalRecord? MedicalRecord { get; set; }

        public Patient(string name, string lastName, string jMBG, DateTime birthDate, string phoneNumber,
            string address, string userName, string password, Gender gender, bool blocked, MedicalRecord? medicalRecord) 
            : base(name, lastName, jMBG, birthDate, phoneNumber, address, userName,password,gender)
        {
            Blocked = blocked;
            MedicalRecord = medicalRecord;
        }
        
        public Patient() { }

        public bool IsAllergic(IEnumerable<string> ingredients)
        {
            return ingredients.Any(x => MedicalRecord.Allergies.Contains(x));
        }

        public override string[] Serialize()
        {
            string[] userValues = base.Serialize();
            string[] patientValues = {Blocked.ToString()};
            string[] medicalRecordValues = {""};
            if (MedicalRecord != null)
                medicalRecordValues = MedicalRecord.Serialize();
            string[] csvValues = userValues.Concat(patientValues).Concat(medicalRecordValues).ToArray();
            return csvValues;
        }

        public override void Deserialize(string[] values)
        {
            Name = values[0];
            LastName = values[1];
            JMBG = values[2];
            BirthDate = Util.ParseDate(values[3]);
            PhoneNumber = values[4];
            Address = values[5];
            Username = values[6];
            Password = values[7];
            Gender = SerialUtil.ParseEnum<Gender>(values[8]);

            Blocked = bool.Parse(values[9]);
            MedicalRecord = new MedicalRecord();
            MedicalRecord.Deserialize(Util.SubArray(values, 10, 4));
        }
    }
}
