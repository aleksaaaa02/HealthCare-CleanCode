using HealthCare.Model;
using HealthCare.Repository;

namespace HealthCare.Service
{
    class AppointmentService : NumericService<Appointment>
    {
        public AppointmentService(string filepath) : base(filepath) { }
        private AppointmentService(IRepository<Appointment> repository) : base(repository) { }

        private static AppointmentService? _instance = null;
        public static AppointmentService GetInstance(IRepository<Appointment> repository)
        {
            if (_instance is not null) return _instance;
            _instance = new AppointmentService(repository);
            return _instance;
        }
    }
}
