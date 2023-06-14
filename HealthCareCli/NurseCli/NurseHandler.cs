using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Exceptions;
using HealthCare.Service.UserService;
using HealthCareCli.CliUtil;
using HealthCare.Service.ScheduleService;
using HealthCare.View;

namespace HealthCareCli.Nurse
{
    public class NurseHandler
    {
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
                string patientJmbg = GetPatient();
                string specialization = GetSpecialization();
                List<string> specialists = GetSpecialists(specialization);
                Appointment appointment = GetAppointment(specialists);
                appointment.PatientJMBG = patientJmbg;

                if (appointment.DoctorJMBG == "")
                    Postpone(GetPostponable(appointment.TimeSlot.Duration,specialists), appointment);

                AddUrgent(appointment);
                Console.WriteLine("Uspesno ste zakazali termin.");
            } catch (ValidationException ve)
            {
                Console.WriteLine(ve.Message);
            }
        }

        private string GetPatient() {
            Console.WriteLine("Izaberite pacijenta");
            PatientService patientService = Injector.GetService<PatientService>();
            List<Patient> patients = patientService.GetAll();

            PrintPatientHeader();
            for (int i = 0; i < patients.Count(); i++)
                PrintPatient(i+1, patients[i]);

            int choice = Input.ReadInt("Redni broj pacijenta: ");

            if (choice < 0 || choice - 1 > patients.Count())
                throw new ValidationException("Pogresan unos.");

            return patients[choice-1].JMBG;
        }

        private List<string> GetSpecialists(string specialization) {
            return Injector.GetService<DoctorService>().GetBySpecialization(specialization);
        }


        private string GetSpecialization() {
            Console.WriteLine("Izaberite specijalizaciju doktora.");
            DoctorService doctorService = Injector.GetService<DoctorService>();
            List<string> specializations = doctorService.GetSpecializations();

            PrintSpecialization(specializations);

            int choice = Input.ReadInt("Redni broj specijalizacije: ");

            if (choice<0 || choice-1> specializations.Count())
                throw new ValidationException("Pogresan unos.");

            return specializations[choice - 1];
        }

        private Appointment GetAppointment(List<string> specialists) {
            Console.WriteLine("Izaberite vrstu termina.");
            Console.WriteLine("1. operacija\n2. pregled");
            int choice = Input.ReadInt("Izbor: ");
            TimeSpan span = new TimeSpan(0, 15, 0);

            if (choice != 1 && choice != 2)
                throw new ValidationException("Pogresan unos");

            if (choice == 1) {
                Console.WriteLine("Unesite duzinu operacije u minutima.");

                int duration = Input.ReadInt("Duzina trajanja: ");
                span = new TimeSpan(0,duration,0);
            }

            Appointment appointment = new Appointment();
            appointment.TimeSlot = new TimeSlot(DateTime.Now, span);
            appointment.IsOperation = choice == 1;

            Appointment? urgent = Injector.GetService<Schedule>().TryGetUrgent(span, specialists);
            
            if (urgent is not null) {
                appointment.TimeSlot.Start = urgent.TimeSlot.Start;
                appointment.DoctorJMBG = urgent.DoctorJMBG;
            }
                
            return appointment;

        }

        private void AddUrgent(Appointment appointment) {
            Injector.GetService<Schedule>().AddUrgent(appointment);
        }

        private List<Appointment> GetPostponable(TimeSpan duration, List<string> specialists) {
            List<Appointment> postponable = new List<Appointment>();

            foreach (string doctor in specialists)
                postponable.AddRange(Injector.GetService<DoctorSchedule>().GetPostponable(duration,doctor));


            postponable = postponable
                .OrderBy(x => Injector.GetService<Schedule>().GetSoonestTimeSlot(x).Start).ToList();

            return postponable;
        }

        private void Postpone(List<Appointment> postponable, Appointment newAppointment) {
            Console.WriteLine("Izaberite termin koji zelite da pomerite.");

            Console.WriteLine($"{"BR",3} {"PACIJENT",20} {"DOKTOR",12} {"VREME I TRAJANJE",30} {"OPERACIJA?",10}");

            for (int i=0;i<postponable.Count();i++ )
                PrintAppointment(i + 1, postponable[i]);

            int choice = Input.ReadInt("Unesite termin: ");

            if (choice < 0 || choice > postponable.Count)
                throw new ValidationException("Pogresan unos.");

            Appointment appointment = postponable[choice - 1];
            
            newAppointment.DoctorJMBG = appointment.DoctorJMBG;
            newAppointment.TimeSlot.Start = appointment.TimeSlot.Start;
            Injector.GetService<Schedule>().Postpone(appointment);
        }


        private void PrintPatient(int i, Patient patient) {
            Console.WriteLine($"{i, 3} {patient.JMBG, 14} {patient.Name, 12} {patient.LastName, 15}");
        }

        private void PrintPatientHeader()
        {
            Console.WriteLine($"{"BR", 3} {"JMBG", 14} {"IME", 12} {"PREZIME", 15}");
        }

        private void PrintSpecialization(List<string> specializations) {
            for (int i = 0; i < specializations.Count(); i++)
                Console.WriteLine(i + 1 + ". " + specializations[i]);
        }

        private void PrintAppointment(int i,Appointment appointment) {
            Patient patient = Injector.GetService<PatientService>().Get(appointment.PatientJMBG);
            Doctor doctor = Injector.GetService<DoctorService>().Get(appointment.DoctorJMBG);

            Console.WriteLine($"{i,3} {patient.Name + " " + patient.LastName,20} {doctor.Name, 12} " +
                $"{appointment.TimeSlot, 30} {ViewUtil.Translate(appointment.IsOperation), 10}");
        }
    }
}
