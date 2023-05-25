using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    public class SplittingRenovationService : NumericService<SplittingRenovation>
    {
        public SplittingRenovationService(string filepath) : base(filepath) { }
    }
}
