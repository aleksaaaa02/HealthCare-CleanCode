using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    public class NotificationService : NumericService<Notification>
    {
        public NotificationService(string filepath) : base(filepath) { }

        public List<Notification> GetForUser(string userJmbg)
        {
            return GetAll().FindAll(x => !x.Seen && x.UserJmbgs.Contains(userJmbg));
        }
    }
}
