using HealthCare.Application.Common;
using HealthCare.Serialize;
using HealthCare.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Model
{
    public class Doctor : User
    {
        public string Specialization { get; set; }
        public int Rating { get; set; }
        public Doctor() : base()
        {
            Specialization = "";
            Random rnd = new Random();
            Rating = rnd.Next(1, 6);
        }
        public Doctor(string name, string lastName, string jMBG, DateTime birthDate, string phoneNumber, string address, string userName, string password, Gender gender, string specialization)
            : base(name, lastName, jMBG, birthDate, phoneNumber, address, userName, password, gender)
        {
            Specialization = specialization;
            Random rnd = new Random();
            Rating = rnd.Next(1, 6);
        }

        public bool IsCapable(string NeededSpecialization)
        {
            return Specialization == NeededSpecialization;
        }

        public override string[] Serialize()
        {
            string[] userValues = base.Serialize();
            return userValues.Concat(new string[] { Specialization }).ToArray();
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

            Specialization = values[9];
        }
    }
}
