using HealthCare.Model;

namespace HealthCare.Service.ScheduleService.Availability
{
    public interface IAvailable<T>
    {
        public bool IsAvailable(T key, TimeSlot timeSlot);

    }
}
