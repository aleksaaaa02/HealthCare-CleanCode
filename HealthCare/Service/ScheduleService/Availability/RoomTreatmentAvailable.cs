using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service.RenovationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service.ScheduleService.Availability
{
    public class RoomTreatmentAvailable : IAvailable<int>
    {
        private readonly TreatmentService _treatmentService;

        public RoomTreatmentAvailable()
        {
            _treatmentService = Injector.GetService<TreatmentService>();
        }

        public bool IsAvailable(int key, TimeSlot timeSlot)
        {
            return _treatmentService.GetAll()
                .Where(t => t.RoomId == key)
                .Count(x => x.TreatmentDuration.Overlaps(timeSlot)) < RoomService.PATIENTCARE_CAPACITY;
        }
    }
}
