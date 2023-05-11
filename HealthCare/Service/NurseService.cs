using HealthCare.Model;
using HealthCare.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HealthCare.Service
{
    public class NurseService : Service<User>
    {
        public NurseService(string filepath) : base(filepath) { }

        public User? GetByUsername(string username)
        {
            return GetAll().Find(x => x.UserName == username);
        }
    }
}
