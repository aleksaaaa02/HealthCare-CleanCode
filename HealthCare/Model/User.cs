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
    public class User
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string JMBG { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber {get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        

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
