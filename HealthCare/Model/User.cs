using HealthCare.Repository;
using HealthCare.Serialize;
using System;

namespace HealthCare.Model
{
    public enum Gender
    {
        Male,
        Female
    }
    public class User : RepositoryItem
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

        public User() : this("", "", "", DateTime.MinValue, "", "", "", "", Gender.Female) { }
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

        public override object Key
        {
            get => JMBG;
            set { JMBG = (string)value; }
        }

        public override string[] Serialize()
        {
            return new string[] { 
                Name, LastName, JMBG, Utility.ToString(BirthDate), 
                PhoneNumber, Address, UserName, Password, Gender.ToString() };
        }

        public override void Deserialize(string[] values)
        {
            Name = values[0];
            LastName = values[1];
            JMBG = values[2];
            BirthDate = Utility.ParseDate(values[3]);
            PhoneNumber = values[4];
            Address = values[5];
            UserName = values[6];
            Password = values[7];
            Gender = Utility.Parse<Gender>(values[8]);
        }
    }
}
