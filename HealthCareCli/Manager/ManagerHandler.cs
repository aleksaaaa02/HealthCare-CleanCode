using HealthCare.Application;
using HealthCare.Service.RenovationService;
using HealthCareCli.CliUtil;
using HealthCareCli.Renovation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareCli.Manager
{
    public class ManagerHandler
    {
        public void Show()
        {
            string input;
            while (true)
            {
                Console.WriteLine("============ MENADŽER ============\n");
                Console.WriteLine("1 Složeno renoviranje");
                Console.WriteLine("q Odjava");

                input = Input.ReadLine("\nOpcija: ").ToLower();

                switch (input)
                {
                    case "1":
                        new RenovationHandler().Show();
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
