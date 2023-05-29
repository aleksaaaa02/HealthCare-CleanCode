using HealthCare.Application.Common;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace HealthCare.Serialize
{
    public static class SerialUtil
    {
        public static T ParseEnum<T>(string str)
        {
            return (T)Enum.Parse(typeof(T), str);
        }

        public static string ToString(IEnumerable<string> data, char separator = '|')
        {
            return string.Join(separator, data);
        }
    }
}
