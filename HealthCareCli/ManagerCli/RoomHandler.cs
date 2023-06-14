using HealthCare.Application;
using HealthCare.Application.Common;
using HealthCare.Application.Exceptions;
using HealthCare.Core.Interior;
using HealthCare.WPF.Common;
using HealthCareCli.CliUtil;

namespace HealthCareCli.Manager
{
    public class RoomHandler
    {
        public string RoomHeader => $"{"BR",3} {"ID",3} {"NAZIV",15} {"TIP",15}";

        public string RoomToString(int i, Room r)
        {
            return $"{i,3} {r.Id,3} {r.Name,15} {ViewUtil.Translate(r.Type),15}";
        }

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
                Console.WriteLine(RoomHeader);
                foreach (var (r, i) in Util.WithIndex(rooms))
                    Console.WriteLine(RoomToString(i, r));

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