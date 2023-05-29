using HealthCare.Model;
using HealthCare.Repository;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service
{
    public class PatientService : Service<Patient>
	{
        public PatientService(IRepository<Patient> repository) : base(repository) { }
    }
}
