using HealthCare.Model;
using HealthCare.Model.Renovation;
using System;

namespace HealthCare.Service.RenovationService
{
    public class SimpleRenovationService : RenovationService<Renovation>
    {
        public SimpleRenovationService(string filepath) : base(filepath) { }

        public override void Execute(Renovation renovation)
        {
            renovation.Executed = true;
            Update(renovation);
        }

        internal bool RoomFree(int id, TimeSlot slot)
        {
            return true;
        }
    }
}
