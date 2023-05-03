using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.ViewModel.ManagerViewModel
{
    public class OrderItemViewModel
    {
        private readonly Equipment _equipment;
        public bool Selected { get; set; }
        public string EquipmentName => _equipment.Name;
        public string EquipmentType => _equipment.TranslateType();
        public string RoomName => _equipment.Name;
        public string RoomType => _equipment.TranslateType();
        public string CurrentQuantity { get; }
        public int OrderQuantity { get; set; }
        public OrderItemViewModel(Equipment equipment, int currentQuantity)
        {
            _equipment = equipment;
            Selected = false;
            CurrentQuantity = currentQuantity.ToString();
            OrderQuantity = 0;
        }
    }

}
