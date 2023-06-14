using HealthCare.Model;
using HealthCare.ViewModel.PatientViewModell;
using HealthCareCli.CliUtil;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthCareCli.Patient
{
    public class PriorityAppointmentHandler
    {
        AppointmentPriorityViewModel viewModel = new AppointmentPriorityViewModel();


        public void Show()
        {
            DateTime date;
            int startHours, startMinutes, endHours, endMinutes;
            string priority;

            while (true) {
                while (true)
                {
                    Console.Write("Unesite krajnji datum (DD-MM-YYYY): ");
                    if (DateTime.TryParse(Console.ReadLine(), out date))
                    {
                        if (date < DateTime.Now) Console.WriteLine("Datum koji ste uneli je u proslosti, pokusajte ponovo");
                        break;
                    }
                    Console.WriteLine("Neispravan datum. Molimo Vas pokusajte ponovo");
                }


                while (true)
                {
                    Console.Write("Unesite pocetno vreme (HH:MM): ");
                    string startTimeInput = Console.ReadLine();
                    if (TryParseTime(startTimeInput, out startHours, out startMinutes))
                    {
                        break;
                    }
                    Console.WriteLine("Neispravan format vremena. Molimo Vas pokusajte ponovo");
                }

                // Input validation for end time (hours and minutes)
                while (true)
                {
                    Console.Write("Unesite krajnje vreme (HH:MM): ");
                    string endTimeInput = Console.ReadLine();
                    if (TryParseTime(endTimeInput, out endHours, out endMinutes))
                    {
                        break;
                    }
                    Console.WriteLine("Neispravan format vremena. Molimo Vas pokusajte ponovo");
                }


                while (true)
                {
                    Console.WriteLine("\nIzaberite doktora (Upisite rednji broj koji se nalazi ispred zeljenog doktora)");
                    int counter = 0;
                    List<Doctor> doctors = viewModel.Doctors;
                    foreach(Doctor doctor in doctors)
                    {
                        Console.WriteLine(counter + ". " + doctor.Name + " " + doctor.LastName + " Specijalizacija: " + doctor.Specialization +" Prosecna ocena:" + doctor.Rating);
                        counter++;
                    }
                    int selectedDoctorIndex;
                    if (!int.TryParse(Console.ReadLine(), out selectedDoctorIndex))
                    {
                        Console.WriteLine("Neispravan broj");
                        continue;
                    }
                    if (selectedDoctorIndex >= doctors.Count)
                    {
                        Console.WriteLine("Neispravan broj");
                        continue;
                    }

                    viewModel.SelectedDoctor = doctors[selectedDoctorIndex];
                    break;

                }




                while (true)
                {
                    Console.Write("\nUnesite rednji broj prioriteta:\n1.Doktor\n2.Datum\n");
                    string inputPriority = Console.ReadLine();
                    if(inputPriority == "1")
                    {
                        priority = "Doctor";
                        break;
                    }
                    else if(inputPriority == "2")
                    {
                        priority = "Date";
                        break;
                    }
                    else
                    {

                    }
                }


                viewModel.EndDate = date;
                viewModel.HourStart = startHours;
                viewModel.MinuteStart = startMinutes;
                viewModel.HourEnd = endHours;
                viewModel.MinuteEnd = endMinutes;
                if (priority == "Doctor") viewModel.IsDoctorPriority=true;
                else viewModel.IsDoctorPriority = false ;

                if (!viewModel.ValidateAllData())
                {
                    Console.WriteLine("Nesto od podataka niste uneli dobro");
                    continue;
                }

                viewModel.CalculateAppointments();
                
                while (true)
                {
                    Console.WriteLine("\nIzaberite pregled (Upisite rednji broj koji se nalazi ispred zeljenog doktora)");
                    int counter = 0;
                    List<Appointment> appointments = viewModel.ResultAppointments.ToList();
                    foreach (Appointment appointment in appointments)
                    {
                        Console.WriteLine(appointment.AppointmentID + " Datum:" + appointment.TimeSlot.Start.ToString() + " Trajanje" + appointment.TimeSlot.Duration.ToString());
                        counter++;
                    }
                    int selectedAppointmendIndex;
                    if (!int.TryParse(Console.ReadLine(), out selectedAppointmendIndex))
                    {
                        Console.WriteLine("Neispravan broj");
                        continue;
                    }
                    if (selectedAppointmendIndex >= appointments.Count)
                    {
                        Console.WriteLine("Neispravan broj");
                        continue;
                    }

                    viewModel.SelectedAppointment = appointments[selectedAppointmendIndex];
                    viewModel.CreateAppointment();
                    Console.WriteLine("Uspesno kreiran pregled");
                    break;

                }
                break;

            }
        }


        private bool TryParseTime(string timeInput, out int hours, out int minutes)
        {
            hours = 0;
            minutes = 0;

            if (string.IsNullOrWhiteSpace(timeInput))
            {
                return false;
            }

            string[] parts = timeInput.Split(':');
            if (parts.Length != 2 || !int.TryParse(parts[0], out hours) || !int.TryParse(parts[1], out minutes))
            {
                return false;
            }

            if (hours < 0 || hours > 23 || minutes < 0 || minutes > 59)
            {
                return false;
            }

            return true;
        }
    }
}
