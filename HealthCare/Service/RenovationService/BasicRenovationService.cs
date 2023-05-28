using HealthCare.Model;
using HealthCare.Model.Renovation;
using System;

namespace HealthCare.Service.RenovationService
{
    public class BasicRenovationService : RenovationService<BasicRenovation>
    {
        public BasicRenovationService(string filepath) : base(filepath) { }

        public override void Execute(BasicRenovation renovation)
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
