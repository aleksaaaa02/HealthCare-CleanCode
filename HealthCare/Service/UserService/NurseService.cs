using HealthCare.Model;
using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service.UserService
{
    public class NurseService : Service<User>, IUserService
    {
        public NurseService(IRepository<User> repository) : base(repository)
        {
        }

        public List<User> GetAllUsers()
        {
            return GetAll().Cast<User>().ToList();
        }
    }
}
