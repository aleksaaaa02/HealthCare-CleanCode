using HealthCare.Model;
using HealthCare.View;
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
        public readonly Equipment Equipment;
        public readonly Room Room;
        public string EquipmentName => Equipment.Name;
        public string EquipmentType => ViewUtil.Translate(Equipment.Type);
        public string RoomName => Room.Name;
        public string RoomType => ViewUtil.Translate(Room.Type);
        public int Quantity => _item.Quantity;
        public string IsDynamic => ViewUtil.Translate(Equipment.Dynamic);
        public InventoryItemViewModel(InventoryItem item, Equipment equipment, Room room)
        {
            _item = item;
            Equipment = equipment;
            Room = room;
        }
    }
}
