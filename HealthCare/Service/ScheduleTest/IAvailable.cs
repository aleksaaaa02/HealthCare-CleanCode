using HealthCare.Model;

namespace HealthCare.Service.ScheduleTest
{
    public interface IAvailable<T>
    {
        public bool IsAvailable(T key, TimeSlot timeSlot);

    }
}
