using HealthCare.Model;
using HealthCare.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    public class DoctorService : Service<Doctor>
    {
        public DoctorService(string filePath) : base(filePath) { }  

        public Doctor GetAccount(string JMBG)
        {
            return Get(JMBG);
        }

        public List<Doctor> GetAccounts() 
        {
            return GetAll();
        }

        public Doctor? GetByUsername(string username)
        {
            return GetAll().Find(x => x.UserName == username);
        }
    }
}
