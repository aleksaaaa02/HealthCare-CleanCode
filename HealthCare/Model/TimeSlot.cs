using HealthCare.Application.Common;
using HealthCare.Serialize;
using System;

namespace HealthCare.Model
{
    public class TimeSlot
    {
        public DateTime Start { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime End => Start + Duration;

        public TimeSlot() : this(DateTime.MinValue, TimeSpan.Zero) { }
        public TimeSlot(TimeSlot other) : this(other.Start, other.Duration) { }
        public TimeSlot(DateTime start, TimeSpan duration)
        {
            Start = start;
            Duration = duration;
        }

        public bool Overlaps(TimeSlot term)
        {
            return InBetweenDates(term.Start, term.End);
        }

        public bool InBetweenDates(DateTime start, DateTime end)
        {
            return start < End && end > Start;
        }

        public bool Contains(DateTime date)
        {
            return Start < date && End > date;
        }

        public override string ToString()
        {
            return Util.ToString(Start) + "|" + Util.ToString(Duration);
        }

        public static TimeSlot Parse(string s)
        {
            string[] tokens = s.Split('|');
            return new TimeSlot(
                Util.ParseDate(tokens[0]),
                Util.ParseDuration(tokens[1]));
        }
    }
}
