using HealthCare.Application;
using HealthCareCli.CliUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareCli.PatientCli
{
    public class PatientHandler
    {
        public void Show()
        {
            string input;
            while (true)
            {
                Console.WriteLine("============ OPCIJE ============\n");
                Console.WriteLine($"Prijavljeni korisnik: {Context.Current.Name} {Context.Current.LastName}\n");
                Console.WriteLine("1 Zakazivanje sa prioritetom");
                Console.WriteLine("q Odjava");

                input = Input.ReadLine("\nOpcija: ").ToLower();

                switch (input)
                {
                    case "1":
                        new PriorityAppointmentHandler().Show();
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
    }
}
