using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    public class JoiningRenovationService : NumericService<JoiningRenovation>
    {
        public JoiningRenovationService(string filepath) : base(filepath) { }
    }
}
