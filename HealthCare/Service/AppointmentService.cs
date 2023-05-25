using HealthCare.Model;
using HealthCare.Repository;

namespace HealthCare.Service
{
    class AppointmentService : NumericService<Appointment>
    {
        public AppointmentService(string filepath) : base(filepath) { }
        public AppointmentService(IRepository<Appointment> repository) : base(repository) { }
    }
}
