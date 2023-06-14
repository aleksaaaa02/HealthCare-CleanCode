using HealthCare.Exceptions;
using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareCli.CliUtil
{
    public static class DefaultHandler
    {
        public static TimeSlot HandleTimeSlotChoice()
        {
            DateTime start = HandleDateTimeChoice("Početni datum: ");
            DateTime end = HandleDateTimeChoice("Krajnji datum: ");
            return new TimeSlot(start, end);
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
