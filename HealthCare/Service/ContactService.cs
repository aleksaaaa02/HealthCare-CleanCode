using HealthCare.Model;
using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    public class ContactService : NumericService<Contact>
    {
        public ContactService(IRepository<Contact> repository) : base(repository)
        {
        }

        public List<Contact> GetForUser(string userJmbg)
        {
            return GetAll().FindAll(x => x.Participants.Contains(userJmbg));
        }
    }
}
