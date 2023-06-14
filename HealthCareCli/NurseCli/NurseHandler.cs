using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Exceptions;
using HealthCare.Service.UserService;
using HealthCareCli.CliUtil;
using HealthCare.Service.ScheduleService;
using HealthCare.View;
using HealthCareCli.NurseCli;

namespace HealthCareCli.Nurse
{
    public class NurseHandler
    {
        private readonly AppointmentHandler _appointmentHandler;
        public void Show()
        {
            string input;
            while (true)
            {
                Console.WriteLine("============ OPCIJE ============\n");
                Console.WriteLine($"Prijavljeni korisnik: {Context.Current.Name} {Context.Current.LastName}\n");
                Console.WriteLine("1 Zakazivanje hitnih operacija i pregleda");
                Console.WriteLine("q Odjava");

                input = Input.ReadLine("\nOpcija: ").ToLower();

                switch (input)
                {
                    case "1":
                        HandleUrgentAppointments();
                        break;
                    case "q":
                        Context.Reset();
                        Console.WriteLine("\n\n");
                        return;
                    default:
                        Console.WriteLine("Nepostojeća opcija. Pokušajte ponovo.\n");
                        break;
                }
            }
        }

        private void HandleUrgentAppointments() {
            try
            {
                string patientJmbg = _appointmentHandler.GetPatient();
                string specialization = _appointmentHandler.GetSpecialization();
                List<string> specialists = _appointmentHandler.GetSpecialists(specialization);
                Appointment appointment = _appointmentHandler.GetAppointment(specialists);
                appointment.PatientJMBG = patientJmbg;

                if (appointment.DoctorJMBG == "")
                    _appointmentHandler.Postpone(appointment, specialists);

                _appointmentHandler.AddUrgent(appointment);
                Console.WriteLine("Uspesno ste zakazali termin.");
            } catch (ValidationException ve)
            {
                Console.WriteLine(ve.Message);
            }
        }
    }
}
