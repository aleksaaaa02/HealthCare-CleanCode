using HealthCare.Model;
using HealthCare.View;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.ViewModel.ManagerViewModel
{
    internal class InventoryFilter
    {
        private List<InventoryItemViewModel> _items;

        public InventoryFilter(List<InventoryItemViewModel> items)
        {
            _items = items;
        }

        public List<InventoryItemViewModel> GetFiltered()
        {
            return _items;
        }

        public void FilterQuantity(bool[] search)
        {
            if (!(search[0] || search[1] || search[2])) return;

            _items = _items.Where(x =>
                search[0] && x.Quantity == 0 ||
                search[1] && x.Quantity <= 10 ||
                search[2] && x.Quantity > 10).ToList();
        }

        public void FilterEquipmentType(bool[] search)
        {
            if (!(search[3] || search[4] || search[5] || search[6])) return;

            _items = _items.Where(x =>
                search[3] && x.Equipment.Type.Equals(EquipmentType.Examinational) ||
                search[4] && x.Equipment.Type.Equals(EquipmentType.Operational) ||
                search[5] && x.Equipment.Type.Equals(EquipmentType.RoomFurniture) ||
                search[6] && x.Equipment.Type.Equals(EquipmentType.HallwayFurniture)).ToList();
        }

        public void FilterRoomType(bool[] search)
        {
            if (!(search[7] || search[8] || search[9] || search[10] || search[11])) return;

            _items = _items.Where(x => 
                search[7] && x.Room.Type.Equals(RoomType.Examinational) ||
                search[8] && x.Room.Type.Equals(RoomType.Operational) ||
                search[9] && x.Room.Type.Equals(RoomType.PatientCare) ||
                search[10] && x.Room.Type.Equals(RoomType.Reception) ||
                search[11] && x.Room.Type.Equals(RoomType.Warehouse)).ToList();
        }

        public void FilterAnyProperty(string query)
        {
            if (query == "") return;
            string[] tokens = query.Split(' ');

            _items = _items.Where(x => 
                HasAllTokens(x, tokens)).ToList();
        }

        private bool HasAllTokens(InventoryItemViewModel item, string[] tokens)
        {
            tokens = NormalizeTokens(tokens);
            return tokens.Count(x =>
                    ContainsToken(item.Equipment.Name, x) ||
                    ContainsToken(item.Room.Name, x) ||
                    ContainsToken(item.Quantity.ToString(), x) ||
                    ContainsToken(Utility.Translate(item.Equipment.Type), x) ||
                    ContainsToken(Utility.Translate(item.Room.Type), x) ||
                    ContainsToken(Utility.Translate(item.Equipment.IsDynamic), x))
                == tokens.Length;
        }

        private string[] NormalizeTokens(string[] tokens)
        {
            return tokens.Select(x => x.Trim().ToLower()).Where(x => x != "").ToArray();
        }

        private bool ContainsToken(string text, string token)
        {
            return text.ToLower().Contains(token);
        }
    }
}
