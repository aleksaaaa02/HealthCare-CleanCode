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
        

        public Hospital(string name)
        {
            Name = name; 
        }

    }
}
