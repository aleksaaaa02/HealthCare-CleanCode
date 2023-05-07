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
            return term.Start <= GetEnd() && term.GetEnd() >= Start;
        }

        public bool InBetweenDates(DateTime start, DateTime end)
        {
            return start < GetEnd() && end > Start;
        }

        public override string ToString()
        {
            return Start.ToString() + " " + Duration.ToString();
        }

        public static TimeSlot Parse(string s)
        {
            string[] tokens = s.Split(' ');
            return new TimeSlot(
                DateTime.Parse(string.Join(" ", new string[] { tokens[0], tokens[1], tokens[2] })),
                TimeSpan.Parse(tokens[3]));
        }
    }
}
