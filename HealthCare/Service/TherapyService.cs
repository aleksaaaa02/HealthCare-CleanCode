using HealthCare.Model;
using HealthCare.Repository;
using System.Threading;

namespace HealthCare.Service
{
    public class TherapyService : NumericService<Therapy>
    {
        public TherapyService(string filepath) : base(filepath) {}
        private TherapyService(IRepository<Therapy> repository) : base(repository) { }

        private static TherapyService? _instance = null;
        public static TherapyService GetInstance(IRepository<Therapy> repository)
        {
            if (_instance is not null) return _instance;
            _instance = new TherapyService(repository);
            return _instance;
        }
    }
}
