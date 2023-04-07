using HealthCare.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HealthCare.Model
{
    internal class Appointment
    {
        public Patient? Patient;
        public Doctor? Doctor;
        public TimeSlot? TimeSlot;

    }
}
