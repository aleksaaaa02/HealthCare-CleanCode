using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    public class RenovationService : NumericService<Renovation>
    {
        private readonly Inventory _inventory;
        public RenovationService(string filepath, Inventory inventory) : base(filepath)
        {
            _inventory = inventory;
        }

        public void ScheduleRenovation(int roomId, TimeSpan span)
        {
            // Schedule.OccupyRoom(new RoomAppointment(roomId, span));
        }

        internal bool RoomFree(int roomId, TimeSlot slot)
        {
            return true;
        }
    }
}
