using HealthCare.Exceptions;
using HealthCare.Model;

namespace HealthCareCli.CliUtil
{
    public static class DefaultHandler
    {
        public static TimeSlot HandleTimeSlotChoice()
        {
            while (true)
            {
                DateTime start = HandleDateTimeChoice("Početni datum: ");
                DateTime end = HandleDateTimeChoice("Krajnji datum: ");
                if (start < end)
                    return new TimeSlot(start, end);

                Console.WriteLine("Krajnji datum mora da bude pre početnog");
            }
        }

        public static DateTime HandleDateTimeChoice(string prompt)
        {
            while (true)
            {
                try
                {
                    return Input.ReadDate(prompt);
                }
                catch (ValidationException ve)
                {
                    Console.WriteLine(ve.Message);
                }
            }
        }
    }
}
