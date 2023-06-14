using HealthCare.Application.Common;
using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;
using HealthCareCli.CliUtil;
using HealthCare.Exceptions;

namespace HealthCareCli.Manager
{
    public class RoomHandler
    {
        public Room HandleRoomCreation()
        {
            string name = Input.ReadLine("Naziv sobe: ");
            Console.WriteLine("Tipovi soba\ns");
            RoomType type = HandleRoomTypeChoice();

            return new Room(0, name, type);
        }

        public int HandleRoomChoice()
        {
            var rooms = Injector.GetService<RoomService>().GetAll();

            while (true)
            {
                Console.WriteLine();
                foreach (var (r, i) in Util.WithIndex(rooms))
                    Console.WriteLine($"{i} {r.Id} {r.Name} {ViewUtil.Translate(r.Type)}");

                try
                {
                    int choice = Input.ReadInt("Izbor: ", "Nepostojeca opcija.");
                    if (choice < 0 || choice >= rooms.Count)
                        throw new ValidationException("Nepostojeca opcija.");

                    return rooms[choice].Id;
                }
                catch (ValidationException ve)
                {
                    Console.WriteLine(ve.Message);
                }
            }
        }

        public RoomType HandleRoomTypeChoice()
        {
            List<RoomType> types = Enum
                .GetValues(typeof(RoomType))
                .Cast<RoomType>().ToList();

            while (true)
            {
                foreach (var (t, i) in Util.WithIndex(types))
                    Console.WriteLine($"{i} {ViewUtil.Translate(t)}");
                try
                {
                    int choice = Input.ReadInt("Izbor: ", "Nepostojeca opcija.");
                    if (choice < 0 || choice >= types.Count)
                        throw new ValidationException("Nepostojeca opcija.");

                    return types[choice];
                }
                catch (ValidationException ve)
                {
                    Console.WriteLine(ve.Message);
                }
            }
        }
    }
}
