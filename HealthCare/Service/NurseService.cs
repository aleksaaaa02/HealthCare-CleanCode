using HealthCare.Model;
using HealthCare.Repository;

namespace HealthCare.Service
{
    public class NurseService : Service<User>, IUserService
    {
        public NurseService(string filepath) : base(filepath) { }
        public NurseService(IRepository<User> repository) : base(repository) { }

        public User? GetByUsername(string username)
        {
            return GetAll().Find(x => x.UserName == username);
        }

        public UserRole GetRole()
        {
            return UserRole.Nurse;
        }
    }
}
