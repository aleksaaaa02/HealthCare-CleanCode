using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.ViewModel.InventoryItemModel
{
    public class InventoryItemViewModel : ViewModelBase
    {
        private readonly InventoryItem _item;
        public string EquipmentId => _item.EquipmentId.ToString();
        public string RoomId => _item.RoomId.ToString();
        public string Quantity => _item.Quantity.ToString();

        public InventoryItemViewModel(InventoryItem item)
        {
            _item = item;
        }
    }
}
