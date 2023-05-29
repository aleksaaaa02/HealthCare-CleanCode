using HealthCare.Model;
using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    public class UserNotificationService : NumericService<UserNotification>
    {
        public UserNotificationService(IRepository<UserNotification> repository) : base(repository) { }

        public List<UserNotification> GetForUser(string userJmbg)
        {
            return GetAll().FindAll(x => x.patientID == userJmbg);
        }
}
}
