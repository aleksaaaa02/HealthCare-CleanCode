using HealthCare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare
{
    class Doctor : User
    {
        public string Specialization { get; set; }
        public Doctor(string name, string lastName, string jMBG, DateTime birthDate, string phoneNumber, string address, string userName, string password, string specialization) : base(name, lastName, jMBG, birthDate, phoneNumber, address, userName, password)
        {
            Specialization = specialization;

        }


        public bool IsAvailable()
        {
            // TO-DO 
            return true;
        }
        public bool IsCapable(string NeededSpecialization)
        {
            if (Specialization == NeededSpecialization)
            {
                return true;
            }
            return false;
        }
    }
}
