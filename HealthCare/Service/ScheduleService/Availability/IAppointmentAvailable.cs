using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service.ScheduleService.Availability
{
    public interface IAppointmentAvailable
    {
        bool IsAvailable(Appointment appointment);
    }
}
