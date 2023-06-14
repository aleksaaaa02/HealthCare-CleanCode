using HealthCare.Service.RenovationService;
using HealthCareCli.CliUtil;
using HealthCare.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Model;
using HealthCare.Application;
using HealthCare.Service;
using HealthCare.Application.Common;
using HealthCare.View;

namespace HealthCareCli.Renovation
{
    public class RenovationHandler
    {
        private readonly SplittingRenovationService _splittingService;
        private readonly JoiningRenovationService _joiningService;

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
                } catch (ValidationException ve)
                {
                    Console.WriteLine(ve.Message);
                }
            }
        }
    }
}
