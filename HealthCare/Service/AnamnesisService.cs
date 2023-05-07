using HealthCare.Model;
using HealthCare.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    public class AnamnesisService
    {
        public List<Anamnesis> Anamneses = new List<Anamnesis>();
        private CsvStorage<Anamnesis> csvStorage;

        public AnamnesisService(string filepath)
        {
            csvStorage = new CsvStorage<Anamnesis>(filepath);
        }

        public void Load()
        {
            Anamneses = csvStorage.Load();
        }

        public void Save()
        {
            csvStorage.Save(Anamneses);
        }

        public Anamnesis? GetByID(int ID)
        {
            return Anamneses.Find(x => x.ID == ID);
        }

        public int NextId()
        {
            if (Anamneses.Count == 0)
                return 1;
            return Anamneses.Max(s => s.ID) + 1;
        }

        public int AddAnamnesis(Anamnesis anamnesis)
        {
            int ID = NextId();
            anamnesis.ID = ID;
            Anamneses.Add(anamnesis);
            return ID;
        }
    }
}
