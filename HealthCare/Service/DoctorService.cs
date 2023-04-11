using HealthCare.Model;
using HealthCare.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    public class DoctorService
    {
        public List<Doctor> Doctors = new List<Doctor>();
        private CsvStorage<Doctor> csvStorage;
        

        public DoctorService(string filePath) 
        { 
            csvStorage = new CsvStorage<Doctor>(filePath);
        }  

        public Doctor GetAccount(string JMBG)
        {
            return Doctors.Find(x => x.JMBG == JMBG);
        }

        public void Load()
        {
            Doctors = csvStorage.Load();
        }
        public void Save() 
        {
            csvStorage.Save(Doctors);
        }

        public List<Doctor> GetAccounts() 
        {
            return Doctors;
        }

        public Doctor? GetByUsername(string username)
        {
            return Doctors.Find(x => x.UserName == username);
        }
    }
}
