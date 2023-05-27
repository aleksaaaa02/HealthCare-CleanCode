using HealthCare.Application;
using HealthCare.Application;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Repository;

namespace HealthCare.Service
{
    public enum Role
    {
        Patient, Doctor, Nurse, Manager
    }

    public class LoginService
    {
        private readonly IRepository<Patient> _patientRepository;
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IRepository<User> _nurseRepository;

        public LoginService(
            IRepository<Patient> patientRepository, 
            IRepository<Doctor> doctorRepository, 
            IRepository<User> nurseRepository) 
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _nurseRepository = nurseRepository;
        }

        public Role Login(string username, string password)
        {
            if (ADMIN_USER == username)
            {
                if (ADMIN_PASS != password)
                    throw new WrongPasswordException();
                return Role.Manager;
            }

            (User user, Role role) = GetUser(username);
            if (user.Password != password)
                throw new WrongPasswordException();

            Context.Current = user;
            return role;
        }

        private (User, Role) GetUser(string username)
        {
            User? user;
            user = _patientRepository.Load().Find(u => u.Username == username);
            if (user != null) return (user, Role.Patient);

            user = _doctorRepository.Load().Find(u => u.Username == username);
            if (user != null) return (user, Role.Doctor);

            user = _nurseRepository.Load().Find(u => u.Username == username);
            if (user != null) return (user, Role.Nurse);

            throw new UsernameNotFoundException();
        }

        private const string ADMIN_USER = "admin";
        private const string ADMIN_PASS = "admin";
    }
}
