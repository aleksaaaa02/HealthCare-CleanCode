using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Application.Common
{
    public static class Util
    {
        public static DateTime ParseDate(string str)
        {
            return DateTime.ParseExact(str, Formats.DATETIME, CultureInfo.InvariantCulture);
        }

        public static TimeSpan ParseDuration(string str)
        {
            return TimeSpan.ParseExact(str, Formats.TIMESPAN, CultureInfo.InvariantCulture);
        }

        public static string ToString(DateTime dt)
        {
            return dt.ToString(Formats.DATETIME);
        }

        public static string ToString(TimeSpan dt)
        {
            return dt.ToString(Formats.TIMESPAN);
        }

        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}
