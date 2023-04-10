using HealthCare.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    public class Hospital
    {   
        public string Name { get; set; }
        public DoctorService DoctorService;
        

        public Hospital(string name)
        {
            Name = name;
            DoctorService = new DoctorService();
        }

    }
}
