using HealthCare.Model;
using HealthCare.Repository;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace HealthCare.Service
{
    public class AnamnesisService : NumericService<Anamnesis>
    {
        public AnamnesisService(string filepath) : base(filepath) { }
        public AnamnesisService(IRepository<Anamnesis> repository) : base(repository) { }

        public int AddAnamnesis(Anamnesis anamnesis)
        {
            Add(anamnesis);
            return anamnesis.ID;
        }
    }
}
