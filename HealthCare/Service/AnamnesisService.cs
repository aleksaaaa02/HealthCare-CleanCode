using HealthCare.Model;

namespace HealthCare.Service
{
    public class AnamnesisService : NumericService<Anamnesis>
    {
        public AnamnesisService(string filepath) : base(filepath) { }

        public Anamnesis? GetByID(int ID)
        {
            return Get(ID);
        }

        public int AddAnamnesis(Anamnesis anamnesis)
        {
            Add(anamnesis);
            return anamnesis.ID;
        }
    }
}
