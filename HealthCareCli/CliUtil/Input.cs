using HealthCare.Application.Common;
using HealthCare.Model;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace HealthCareCli.CliUtil
{
    public static class Input
    {
        private const string defaultError = "Unos nije validan.";
        private const string formatError = "Format nije validan.";

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

        public static DateTime ReadDate(string prompt, string err = formatError)
        {
            try
            {
                return Util.ParseDate(ReadLine(prompt), Formats.SHORTDATETIME);
            } catch (FormatException) 
            { 
                throw new ValidationException(err);
            }
        }
    }
}
