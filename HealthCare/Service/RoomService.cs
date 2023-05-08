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
    public class RoomService : Service<Room>
    {
        public RoomService(string filepath) : base(filepath) { }

        internal int GetWarehouseId()
        {
            Room? warehouse = GetAll().Find(x => x.Type == RoomType.Warehouse);
            if (warehouse is null) throw new KeyNotFoundException();
            return warehouse.Id;
        }
    }
}
