using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    public class TimeSlot
    {
        public DateTime Start { get; set; }
        public TimeSpan Duration { get; set; }

        public TimeSlot(DateTime start, TimeSpan duration)
        {
            Start = start;
            Duration = duration;
        }

        public DateTime GetEnd() 
        {
            return Start + Duration;
        }
        public bool Overlaps(TimeSlot term)

        {
            return term.Start < GetEnd() && term.GetEnd() > Start;
        }

    }
}
