using HealthCare.Model;
using HealthCare.Repository;

namespace HealthCare.Service
{
    public class EquipmentService : NumericService<Equipment>
    {
        public EquipmentService(string filepath) : base(filepath) { }
        public EquipmentService(IRepository<Equipment> repository) : base(repository) { }
    }
}
