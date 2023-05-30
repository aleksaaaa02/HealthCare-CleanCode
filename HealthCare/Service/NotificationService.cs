using System.Collections.Generic;
using HealthCare.Model;
using HealthCare.Repository;

namespace HealthCare.Service
{
    public class NotificationService : NumericService<Notification>
    {
        public NotificationService(IRepository<Notification> repository) : base(repository)
        {
        }

        public List<Notification> GetForUser(string userJmbg)
        {
            return GetAll().FindAll(x => !x.Seen && x.Recipients.Contains(userJmbg));
        }
    }
}