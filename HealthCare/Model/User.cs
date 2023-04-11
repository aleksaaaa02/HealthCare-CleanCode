using HealthCare.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    public enum Gender
    {
        Male,
        Female
    }
    public class User : ISerializable
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string JMBG { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber {get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }

        public User(string name, string lastName, string jMBG, DateTime birthDate, string phoneNumber, string address, string userName, string password, Gender gender)
        {
            Name = name;
            LastName = lastName;
            JMBG = jMBG;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            Address = address;
            UserName = userName;
            Password = password;
            Gender = gender;
        }

        public User() : this("", "", "", DateTime.Now, "", "", "", "", Gender.Female)
        {
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Name, LastName, JMBG, BirthDate.ToString(), PhoneNumber, Address, UserName, Password, Gender.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Name = values[0];
            LastName = values[1];
            JMBG = values[2];
            BirthDate = DateTime.Parse(values[3]);
            PhoneNumber = values[4];
            Address = values[5];
            UserName = values[6];
            Password = values[7];
            Gender = (Gender) Enum.Parse(typeof(Gender), values[8]);
        }
    }
}
