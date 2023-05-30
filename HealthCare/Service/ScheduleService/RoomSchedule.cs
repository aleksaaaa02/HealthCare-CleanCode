using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service.ScheduleService.Availability;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service.ScheduleService
{
    public class RoomSchedule : ScheduleBase<int>, IAppointmentAvailable
    {
        private readonly RoomService _roomService;

        public RoomSchedule()
        {
            _roomService = Injector.GetService<RoomService>();
            _availabilityValidators = new List<IAvailable<int>> {
               new RoomRenovationAvailable(),
               new RoomAppointmentAvailable()
            };
        }

        public void SetFirstAvailableRoom(Appointment appointment)
        {
            RoomType type = appointment.IsOperation ? RoomType.Operational 
                : RoomType.Examinational;

            appointment.RoomID = _roomService
                .GetRoomsByType(type)
                .Where(r => IsAvailable(r.Id, appointment.TimeSlot))
                .Select(r => r.Id)
                .FirstOrDefault(0);
        }

        public bool IsAvailable(Appointment appointment)
        {
            return IsAvailable(appointment.RoomID, appointment.TimeSlot);
        }
    }
}
