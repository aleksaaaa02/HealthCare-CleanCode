using HealthCare.Model;

namespace HealthCare.Service
{
    public class RenovationService : NumericService<Renovation>
    {
        public RenovationService(string filepath) : base(filepath) { }

        public bool RoomFree(int id, TimeSlot slot)
        {
            return true;
        }
    }
}
