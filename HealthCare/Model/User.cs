using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    internal class User
    {
        public string Name;
        public string LastName;
        public string JMBG;
        public DateTime BirthDate;
        public string PhoneNumber;
        public string Address;
        public string UserName;
        public string Password;
        public enum Gender{ 
            Male,
            Female
        }

        public User(string name, string lastName, string jMBG, DateTime birthDate, string phoneNumber, string address, string userName, string password)
        {
            Name = name;
            LastName = lastName;
            JMBG = jMBG;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            Address = address;
            UserName = userName;
            Password = password;
        }
    }
}
