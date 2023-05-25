using HealthCare.Model;
using HealthCare.Repository;

namespace HealthCare.Service
{
    public class NurseService : Service<User>, IUserService
    {
        public NurseService(string filepath) : base(filepath) { }
        private NurseService(IRepository<User> repository) : base(repository) { }

        private static NurseService? _instance = null;
        public static NurseService GetInstance(IRepository<User> repository)
        {
            if (_instance is not null) return _instance;
            _instance = new NurseService(repository);
            return _instance;
        }

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
