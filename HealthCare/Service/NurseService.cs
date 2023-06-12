using HealthCare.Model;
using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    public class NurseService : Service<User>
    {
        public NurseService(IRepository<User> repository) : base(repository)
        {

        }
    }
}
