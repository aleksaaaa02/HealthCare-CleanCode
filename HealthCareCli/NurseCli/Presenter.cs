using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Application;
using HealthCare.Service.UserService;
using HealthCare.View;

namespace HealthCareCli.NurseCli
{
    public static class Presenter
    {
        public static void PrintPatient(int i, Patient patient)
        {
            Console.WriteLine($"{i,3} {patient.JMBG,14} {patient.Name,12} {patient.LastName,15}");
        }

        public static void PrintPatientHeader()
        {
            Console.WriteLine($"{"BR",3} {"JMBG",14} {"IME",12} {"PREZIME",15}");
        }

        public static void PrintSpecialization(List<string> specializations)
        {
            for (int i = 0; i < specializations.Count(); i++)
                Console.WriteLine(i + 1 + ". " + specializations[i]);
        }

        public static void PrintAppointment(int i, Appointment appointment)
        {
            Patient patient = Injector.GetService<PatientService>().Get(appointment.PatientJMBG);
            Doctor doctor = Injector.GetService<DoctorService>().Get(appointment.DoctorJMBG);

            Console.WriteLine($"{i,3} {patient.Name + " " + patient.LastName,20} {doctor.Name,12} " +
                              $"{appointment.TimeSlot,30} {ViewUtil.Translate(appointment.IsOperation),10}");
        }
    }
}
