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
    internal class RoomService
    {
        public List<Room> Room { get; set; }
        private CsvStorage<Room> csvStorage;

        public RoomService(string filepath)
        {
            Room = new List<Room>();
            csvStorage = new CsvStorage<Room>(filepath);
        }

        public Room Get(Room Room)
        {
            Room? found = Room.Find(x => x == Room);
            if (found is not null) { return found; }
            else { throw new NonExistingObjectException(); }
        }

        public void Add(Room Room)
        {
            if (Contains(Room)) { throw new DuplicateObjectException(); }
            Room.Add(Room);
        }

        public void Remove(Room Room)
        {
            if (!Contains(Room)) { throw new NonExistingObjectException(); }
            Room.Remove(Room);
        }

        public void Update(Room Room)
        {
            Room current = Get(Room);
            current.Id = Room.Id;
            current.Name = Room.Name;
            current.Type = Room.Type;
            current.Dynamic = Room.Dynamic;
        }

        public bool Contains(Room Room)
        {
            return Room.Find(x => x.Id == Room.Id) is not null;
        }

        public void Load()
        {
            Room = csvStorage.Load();
        }

        public void Save()
        {
            csvStorage.Save(Room);
        }
    }
}
