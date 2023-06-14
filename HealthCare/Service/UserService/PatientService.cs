using HealthCare.Model;
using HealthCare.Repository;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service.UserService
{
    public class PatientService : Service<Patient>, IUserService
    {
        public PatientService(IRepository<Patient> repository) : base(repository)
        {
        }

        public List<User> GetAllUsers()
        {
            return GetAll().Cast<User>().ToList();
        }
    }
}