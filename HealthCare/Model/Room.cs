using HealthCare.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    public enum RoomType
    {
        Examinational,
        Operational,
        PatientCare,
        Reception,
        Warehouse
    }
    public class Room : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RoomType Type { get; set; }
        public Room() : this("", RoomType.Warehouse) { }
        public Room(string name, RoomType type)
        {
            Id = id;
            Name = name;
            Type = type;
        }

        public string[] ToCSV()
        {
            return new string[] { Name, Type.ToString() };
        }

        public void FromCSV(string[] values)
        {
            Name = values[0];
            Type = (RoomType) Enum.Parse(typeof(RoomType), values[1]);
        }

        public void Copy(Room room)
        {
            Name = room.Name;
            Type = room.Type;
        }
    }
}
