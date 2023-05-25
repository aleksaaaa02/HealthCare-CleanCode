using HealthCare.Model;
using HealthCare.Repository;

namespace HealthCare.Service
{
    public class EquipmentService : NumericService<Equipment>
    {
        public EquipmentService(string filepath) : base(filepath) { }
        private EquipmentService(IRepository<Equipment> repository) : base(repository) { }

        private static EquipmentService? _instance = null;
        public static EquipmentService GetInstance(IRepository<Equipment> repository)
        {
            if (_instance is not null) return _instance;
            _instance = new EquipmentService(repository);
            return _instance;
        }
    }
}
