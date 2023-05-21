using HealthCare.Model;

namespace HealthCare.Service
{
    class AppointmentService : NumericService<Appointment>
    {
        public AppointmentService(string filepath) : base(filepath) { }
    }
}
