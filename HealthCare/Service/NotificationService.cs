using HealthCare.Model;
using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service
{
    public class NotificationService : NumericService<Notification>
    {
        public NotificationService(string filepath) : base(filepath) { }
        private NotificationService(IRepository<Notification> repository) : base(repository) { }

        private static NotificationService? _instance = null;
        public static NotificationService GetInstance(IRepository<Notification> repository)
        {
            if (_instance is not null) return _instance;
            _instance = new NotificationService(repository);
            return _instance;
        }

        public List<Notification> GetForUser(string userJmbg)
        {
            return GetAll().FindAll(x => 
                !x.Seen && x.Recipients.Contains(userJmbg));
        }
    }
}
