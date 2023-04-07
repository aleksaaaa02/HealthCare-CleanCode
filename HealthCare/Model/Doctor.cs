using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    internal class Doctor : User
    {

        public Doctor(string name, string lastName, string jMBG, DateTime birthDate, string phoneNumber, string address, string userName, string password) : base(name, lastName, jMBG, birthDate, phoneNumber, address, userName, password)
        {


        }


        public bool IsAvailable()
        {

            // TO-DO 
            return true;
        }
        public bool IsCapable()
        {
            return true;
        }
    }
}
