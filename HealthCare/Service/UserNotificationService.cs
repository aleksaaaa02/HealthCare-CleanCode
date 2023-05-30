using System.Collections.Generic;
using HealthCare.Model;
using HealthCare.Repository;

namespace HealthCare.Service
{
    public class UserNotificationService : NumericService<UserNotification>
    {
        public UserNotificationService(IRepository<UserNotification> repository) : base(repository)
        {
        }

        public List<UserNotification> GetForUser(string userJmbg)
        {
            return GetAll().FindAll(x => x.patientID == userJmbg);
        }
    }
}