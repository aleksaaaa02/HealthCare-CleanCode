using HealthCare.Model;

namespace HealthCare.Service.ScheduleService.Availability
{
    public interface IAppointmentAvailable
    {
        bool IsAvailable(Appointment appointment);
    }
}