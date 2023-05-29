using HealthCare.Model;

namespace HealthCare.Service.ScheduleService
{
    public interface IAvailable<T>
    {
        public bool IsAvailable(T key, TimeSlot timeSlot);

    }
}
