using System;
using System.Linq;
using HealthCare.Application.Common;
using HealthCare.Serialize;

namespace HealthCare.Model
{
    public class Doctor : User
    {
        public Doctor() : base()
        {
            Specialization = "";
            Random rnd = new Random();
            Rating = rnd.Next(1, 6);
        }

        public Doctor(string name, string lastName, string jMBG, DateTime birthDate, string phoneNumber, string address,
            string userName, string password, Gender gender, string specialization)
            : base(name, lastName, jMBG, birthDate, phoneNumber, address, userName, password, gender)
        {
            Specialization = specialization;
            Random rnd = new Random();
            Rating = rnd.Next(1, 6);
            Color = ColorRandomizer.GetRandomColor();
        }

        public Doctor(string name, string lastName, string jMBG, DateTime birthDate, string phoneNumber, string address,
            string userName, string password, Gender gender, string specialization, string color)
            : base(name, lastName, jMBG, birthDate, phoneNumber, address, userName, password, gender)
        {
            Specialization = specialization;
            Random rnd = new Random();
            Rating = rnd.Next(1, 6);
            Color = color;
        }

        public string Specialization { get; set; }
        public int Rating { get; set; }
        
        public string Color { get; set; }

        public bool IsCapable(string NeededSpecialization)
        {
            return Specialization == NeededSpecialization;
        }

        public override string[] Serialize()
        {
            string[] userValues = base.Serialize();
            return userValues.Concat(new string[] { Specialization, Rating.ToString(), Color }).ToArray();
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
            Rating = int.Parse(values[10]);
            Color = values[11];
        }
    }
}