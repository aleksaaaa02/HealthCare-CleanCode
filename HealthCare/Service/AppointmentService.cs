using HealthCare.Model;
using HealthCare.Repository;

namespace HealthCare.Service
{
    class AppointmentService : NumericService<Appointment>
    {
        public AppointmentService(IRepository<Appointment> repository) : base(repository) { }
    }
}
