using HealthCare.Model;
using HealthCare.Repository;
using System.Collections.Generic;

namespace HealthCare.Service
{
    public class RoomService : NumericService<Room>
    {
        public RoomService(string filepath) : base(filepath) { }
        private RoomService(IRepository<Room> repository) : base(repository) { }

        private static RoomService? _instance = null;
        public static RoomService GetInstance(IRepository<Room> repository)
        {
            if (_instance is not null) return _instance;
            _instance = new RoomService(repository);
            return _instance;
        }

        public int GetWarehouseId()
        {
            var warehouses = GetRoomsByType(RoomType.Warehouse);
            if (warehouses.Count == 0) 
                throw new KeyNotFoundException();

            return warehouses[0].Id;
        }

        public List<Room> GetRoomsByType(RoomType roomType)
        {
            return GetAll().FindAll(x => x.Type == roomType);
        }
    }
}
