using HealthCare.Application.Common;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace HealthCare.Serialize
{
    public static class Utility
    {
        public static DateTime ParseDate(string str)
        {
            return DateTime.ParseExact(str, Formats.DATETIME, CultureInfo.InvariantCulture);
        }

        internal static TimeSpan ParseDuration(string str)
        {
            return TimeSpan.ParseExact(str, Formats.TIMESPAN, CultureInfo.InvariantCulture);
        }

        public static T Parse<T>(string str)
        {
            return (T)Enum.Parse(typeof(T), str);
        }

        public static string ToString(DateTime dt)
        {
            return dt.ToString(Formats.DATETIME);
        }
        public static string ToString(TimeSpan dt)
        {
            return dt.ToString(Formats.TIMESPAN);
        }

        public static string ToString<T>(IEnumerable<T> data, char separator = '|')
        {
            return string.Join(separator, data);
        }

        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}
