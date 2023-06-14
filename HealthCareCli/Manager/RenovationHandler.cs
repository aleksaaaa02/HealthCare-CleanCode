using HealthCare.Service.RenovationService;
using HealthCareCli.CliUtil;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Application;
using HealthCare.Model.Renovation;
using HealthCareCli.Exceptions;
using HealthCareCli.Manager;

namespace HealthCareCli.Renovation
{
    public class RenovationHandler
    {
        private readonly RoomHandler roomHandler = new RoomHandler();

        public void Handle()
        {
            try
            {
                TimeSlot slot = DefaultHandler.HandleTimeSlotChoice();
                bool splitting = GetRenovationType();

                if (splitting) HandleSplitting(slot);
                else HandleJoining(slot);
            } catch (ExitSignal)
            {
                Console.WriteLine("Prekid operacije.");
            }
        }

        private void HandleSplitting(TimeSlot slot)
        {
            Console.WriteLine("Izaberite sobu za deljenje\n");
            var roomId = roomHandler.HandleRoomChoice();

            Console.WriteLine("Unos prve nove sobe");
            var newRoom1 = roomHandler.HandleRoomCreation();
            Console.WriteLine("Unos druge nove sobe");
            var newRoom2 = roomHandler.HandleRoomCreation();

            var renovation = new SplittingRenovation(roomId, slot, newRoom1, newRoom2);
            Injector.GetService<SplittingRenovationService>().Add(renovation);
            Console.WriteLine("Uspešno zakazano deljenje sobe.");
        }

        private void HandleJoining(TimeSlot slot)
        {
            Console.WriteLine("Izbor prve sobe za spajanje");
            var roomId1 = roomHandler.HandleRoomChoice();
            Console.WriteLine("Izbor druge sobe za spajanje");
            var roomId2 = roomHandler.HandleRoomChoice();

            Console.WriteLine("Unos sobe koja će biti kreirana");
            var newRoom = roomHandler.HandleRoomCreation();

            var renovation = new JoiningRenovation(roomId1, slot, roomId2, newRoom);
            Injector.GetService<JoiningRenovationService>().Add(renovation);
            Console.WriteLine("Uspešno zakazano deljenje sobe.");
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
