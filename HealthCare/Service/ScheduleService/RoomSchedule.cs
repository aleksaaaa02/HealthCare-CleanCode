using System.Collections.Generic;
using System.Linq;
using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service.ScheduleService.Availability;

namespace HealthCare.Service.ScheduleService
{
    public class RoomSchedule : ScheduleBase<int>, IAppointmentAvailable
    {
        private readonly RoomService _roomService;

        public RoomSchedule()
        {
            _roomService = Injector.GetService<RoomService>();
            _availabilityValidators = new List<IAvailable<int>>
            {
                new RoomRenovationAvailable(),
                new RoomAppointmentAvailable()
            };
        }

        public bool IsAvailable(Appointment appointment)
        {
            return appointment.RoomID == 0 ||
                   IsAvailable(appointment.RoomID, appointment.TimeSlot);
        }

        public void SetFirstAvailableRoom(Appointment appointment)
        {
            RoomType type = appointment.IsOperation
                ? RoomType.Operational
                : RoomType.Examinational;

            appointment.RoomID = _roomService
                .GetRoomsByType(type)
                .Where(r => IsAvailable(r.Id, appointment.TimeSlot))
                .Select(r => r.Id)
                .FirstOrDefault(0);
        }
    }
}