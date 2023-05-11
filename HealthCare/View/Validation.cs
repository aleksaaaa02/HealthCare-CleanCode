using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.View
{
    public class Validation
    {
        public static bool IsNatural(string number)
        {
            return int.TryParse(number, out var n) && n > 0;
        }
    }
}
