using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    public class RoomService
    {
        public List<Room> Rooms { get; set; }
        private CsvStorage<Room> csvStorage;

        public RoomService(string filepath)
        {
            Rooms = new List<Room>();
            csvStorage = new CsvStorage<Room>(filepath);
        }

        public Room Get(string roomName)
        {
            Room? found = Rooms.Find(x => x.Name == roomName);
            if (found != null) { return found; }
            throw new ObjectNotFoundException();
        }

        public void Add(Room room)
        {
            if (Contains(room.Name)) throw new ObjectAlreadyExistException();
            Rooms.Add(room);
        }

        public void Remove(string roomName)
        {
            if (!Contains(roomName)) throw new ObjectNotFoundException();
            Rooms.RemoveAll(x => x.Name == roomName);
        }

        public void Update(Room room)
        {
            if (!Contains(room.Name)) throw new ObjectNotFoundException();
            Room current = Get(room.Name);
            current.Copy(room);
        }

        public bool Contains(string roomName)
        {
            return Rooms.FindIndex(x => x.Name == roomName) >= 0;
        }

        public void Load()
        {
            Rooms = csvStorage.Load();
        }

        public void Save()
        {
            csvStorage.Save(Rooms);
        }
    }
}
