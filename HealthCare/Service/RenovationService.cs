using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    public class RenovationService
    {
        public RenovationService(Inventory inventory)
        {
            // _inventory = inventory;
        }
        public void ScheduleRenovation(int roomId, TimeSpan span)
        {
            // Schedule.OccupyRoom(new RoomAppointment(roomId, span));
        }
    }
}
