using HealthCare.Model;
using HealthCare.Repository;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace HealthCare.Service
{
    public class AnamnesisService : NumericService<Anamnesis>
    {
        public AnamnesisService(string filepath) : base(filepath) { }
        private AnamnesisService(IRepository<Anamnesis> repository) : base(repository) { }

        private static AnamnesisService? _instance = null;
        public static AnamnesisService GetInstance(IRepository<Anamnesis> repository)
        {
            if (_instance is not null) return _instance;
            _instance = new AnamnesisService(repository);
            return _instance;
        }

        public int AddAnamnesis(Anamnesis anamnesis)
        {
            Add(anamnesis);
            return anamnesis.ID;
        }
    }
}
