using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    public enum Genders
    {
        Male,
        Female
    }
<<<<<<< HEAD

=======
<<<<<<< HEAD
=======

>>>>>>> origin/2-1-crud-pregledi-operacije
>>>>>>> 3-1-crud-pregledi
    public class User
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int JMBG { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber {get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
<<<<<<< HEAD
        public Genders Gender { get; set; }

        public User(string name, string lastName, string jMBG, DateTime birthDate, string phoneNumber, string address, string userName, string password, Genders gender)
=======
        
<<<<<<< HEAD

=======
>>>>>>> origin/2-1-crud-pregledi-operacije
        public User(string name, string lastName, string jMBG, DateTime birthDate, string phoneNumber, string address, string userName, string password)
>>>>>>> 3-1-crud-pregledi
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

    }
}
