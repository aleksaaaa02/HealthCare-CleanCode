using HealthCare.Application.Common;
using HealthCare.Exceptions;

namespace HealthCareCli.CliUtil
{
    public static class Input
    {
        private const string defaultError = "Unos nije validan";

        public static string ReadLine(string prompt)
        {
            Console.Write(prompt);
            return (Console.ReadLine() ?? "").Trim();
        }

        public static int ReadInt(string prompt, string err = defaultError)
        {
            int number;
            if (!int.TryParse(ReadLine(prompt), out number)) {
                throw new ValidationException(err);
            }
            return number;
        }

        public static DateTime ReadDate(string prompt, string err = defaultError)
        {
            try
            {
                return Util.ParseDate(ReadLine(prompt));
            } catch (FormatException) 
            { 
                throw new ValidationException(err);
            }
        }
    }
}
