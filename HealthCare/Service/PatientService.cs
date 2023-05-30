using HealthCare.Model;
using HealthCare.Repository;

namespace HealthCare.Service
{
    public class PatientService : Service<Patient>
    {
        public PatientService(IRepository<Patient> repository) : base(repository)
        {
        }
    }
}