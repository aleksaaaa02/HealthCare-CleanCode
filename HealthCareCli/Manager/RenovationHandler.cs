using HealthCare.Application;
using HealthCare.Application.Common;
using HealthCare.Application.Exceptions;
using HealthCare.Core.Interior;
using HealthCare.Core.Interior.Renovation.Service;
using HealthCare.WPF.Common;
using HealthCareCli.CliUtil;

namespace HealthCareCli.Manager
{
    public class RenovationHandler
    {
        private readonly JoiningRenovationService _joiningService;
        private readonly SplittingRenovationService _splittingService;

        public void Handle()
        {
            bool splitting = GetRenovationType();
            if (splitting)
                HandleSplitting();
            else
                HandleJoining();
        }

        private void HandleSplitting()
        {
        }

        private void HandleJoining()
        {
        }

        private Room GetRoom()
        {
            while (true)
            {
                Console.WriteLine("Sobe\n");
                var rooms = Injector.GetService<RoomService>().GetAll();

                foreach (var (r, i) in Util.WithIndex(rooms))
                    Console.WriteLine($"{i} {r.Id} {r.Name} {ViewUtil.Translate(r.Type)}");

                try
                {
                    int choice = Input.ReadInt("Izbor: ", "Nepostojeca opcija.");
                    if (choice != 1 && choice != 2)
                        throw new ValidationException("Nepostojeca opcija.");
                    return new Room();
                }
                catch (ValidationException ve)
                {
                    Console.WriteLine(ve.Message);
                }
            }
        }

        private bool GetRenovationType()
        {
            while (true)
            {
                Console.WriteLine("Tip renoviranja\n");
                Console.WriteLine("1 spajanje");
                Console.WriteLine("2 deljenje\n");

                try
                {
                    int choice = Input.ReadInt("Izbor: ", "Nepostojeća opcija.");
                    if (choice != 1 && choice != 2)
                        throw new ValidationException("Nepostojeća opcija.");

                    return choice == 2;
                }
                catch (ValidationException ve)
                {
                    Console.WriteLine(ve.Message);
                }
            }
        }
    }
}