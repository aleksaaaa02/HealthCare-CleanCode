using HealthCare.Repository;
using System;

namespace HealthCare.Model
{
    public class TimeSlot
    {
        public DateTime Start { get; set; }
        public TimeSpan Duration { get; set; }

        public TimeSlot() : this(DateTime.MinValue, TimeSpan.Zero) { }

        public TimeSlot(DateTime start, TimeSpan duration)
        {
            Start = start;
            Duration = duration;
        }
        public TimeSlot(TimeSlot other)
        {
            Start = other.Start;
            Duration = other.Duration;
        }

        public DateTime GetEnd() 
        {
            return Start + Duration;
        }
        public bool Overlaps(TimeSlot term)

        {
            return term.Start < GetEnd() && term.GetEnd() > Start;
        }

        public bool InBetweenDates(DateTime start, DateTime end)
        {
            return start < GetEnd() && end > Start;
        }

        public override string ToString()
        {
            return Utility.ToString(Start) + "|" + Utility.ToString(Duration);
        }

        public static TimeSlot Parse(string s)
        {
            string[] tokens = s.Split('|');
            return new TimeSlot(
                Utility.ParseDate(tokens[0]),
                Utility.ParseDuration(tokens[1]));
        }
    }
}
