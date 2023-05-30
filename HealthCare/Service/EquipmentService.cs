using HealthCare.Model;
using HealthCare.Repository;

namespace HealthCare.Service
{
    public class EquipmentService : NumericService<Equipment>
    {
        public EquipmentService(IRepository<Equipment> repository) : base(repository)
        {
        }
    }
}