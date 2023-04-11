using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.ViewModel.ManagerViewModel
{
    public class InventoryItemViewModel
    {
        private readonly InventoryItem _item;
        public string EquipmentName => _item.Equipment.Name;
        public string RoomName => _item.Room.Name;
        public string Quantity => _item.Quantity.ToString();
        public InventoryItemViewModel(InventoryItem item)
        {
            _item = item;
        }
    }
}
