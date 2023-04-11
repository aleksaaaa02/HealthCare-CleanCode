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
    public class NurseService
    {
        public List<User> Nurses = new List<User>();
        private CsvStorage<User> csvStorage;

        public NurseService(string filepath)
        {
            csvStorage = new CsvStorage<User>(filepath);
        }

        public void Load()
        {
            Nurses = csvStorage.Load();
        }

        public User? GetByUsername(string username)
        {
            return Nurses.Find(x => x.UserName == username);
        }
    }
}
