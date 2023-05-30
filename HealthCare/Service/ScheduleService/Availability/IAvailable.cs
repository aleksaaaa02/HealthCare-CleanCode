using HealthCare.Model;

namespace HealthCare.Service.ScheduleService.Availability
{
    public interface IAvailable<T>
    {
        bool IsAvailable(T key, TimeSlot timeSlot);
    }
}
