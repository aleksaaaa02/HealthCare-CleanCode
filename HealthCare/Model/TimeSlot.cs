using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare
{
    class TimeSlot
    {
        public DateTime Start { get; set; }
        public TimeSpan Duration { get; set; }

        public TimeSlot(DateTime start, TimeSpan duration)
        {
            Start = start;
            Duration = duration;
        }

        public bool Overlaps(TimeSlot term)
        {
            // TO-DO: Metoda za proveru preklapanja izemdju termina
            return true;
        }

    }
}
