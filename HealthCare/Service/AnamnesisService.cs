using HealthCare.Model;
using HealthCare.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    internal class AnamnesisService : NumericService<Anamnesis>
    {
        public AnamnesisService(string filepath) : base(filepath) { }

        public Anamnesis? GetByID(int ID)
        {
            return Get(ID);
        }

        public int AddAnamnesis(Anamnesis anamnesis)
        {
            int ID = NextId();
            anamnesis.ID = ID;
            Add(anamnesis);
            return ID;
        }
    }
}
