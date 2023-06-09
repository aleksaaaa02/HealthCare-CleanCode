using HealthCare.Model;
using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    public class MessageService : NumericService<Message>
    {
        public MessageService(IRepository<Message> repository) : base(repository)
        {
        }

        public List<Message> GetByContact(int contactID)
        {
            return GetAll().FindAll(x => x.contactID == contactID);
        }
    }
}
