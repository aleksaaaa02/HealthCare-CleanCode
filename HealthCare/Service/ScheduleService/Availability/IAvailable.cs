using HealthCare.Model;

namespace HealthCare.Service.ScheduleService.Availability
{
    public interface IAvailable
    {
        public bool IsAvailable(Appointment key);

    }
}
