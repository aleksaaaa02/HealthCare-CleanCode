using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.ViewModel.ManagerViewModel
{
    internal class InventoryItemFilter
    {
        private List<InventoryItem> _items;

        public InventoryItemFilter(List<InventoryItem> items)
        {
            _items = items;
        }

        public List<InventoryItem> GetFiltered()
        {
            return _items;
        }

        public void FilterAnyProperty(string query)
        {
            if (query == "") return;
            string[] tokens = query.Split(' ');

            List<InventoryItem> filtered = new List<InventoryItem>();
            foreach (var item in _items)
            {
                if (_hasAllTokens(item, tokens))
                { filtered.Add(item); }
            }
            _items = filtered;
        }

        public void FilterQuantity(bool[] quantities)
        {
            if (!(quantities[0] || quantities[1] || quantities[2])) return;

            List<InventoryItem> filtered = new List<InventoryItem>();
            foreach (var item in _items)
            {
                if (quantities[0] && item.Quantity == 0 ||
                    quantities[1] && item.Quantity <= 10 ||
                    quantities[2] && item.Quantity > 10)
                { filtered.Add(item); }
            }
            _items = filtered;
        }

        public void FilterEquipmentType(bool[] types)
        {
            if (!(types[0] || types[1] || types[2] || types[3])) return;

            List<InventoryItem> filtered = new List<InventoryItem>();
            foreach (var item in _items)
            {
                if (types[0] && item.Equipment.Type.Equals(EquipmentType.Examinational) ||
                    types[1] && item.Equipment.Type.Equals(EquipmentType.Operational) ||
                    types[2] && item.Equipment.Type.Equals(EquipmentType.RoomFurniture) ||
                    types[3] && item.Equipment.Type.Equals(EquipmentType.HallwayFurniture))
                { filtered.Add(item); }
            }
            _items = filtered;
        }

        public void FilterRoomType(bool[] types)
        {
            if (!(types[0] || types[1] || types[2] || types[3] || types[4])) return;

            List<InventoryItem> filtered = new List<InventoryItem>();
            foreach (var item in _items)
            {
                if (types[0] && item.Room.Type.Equals(RoomType.Examinational) ||
                    types[1] && item.Room.Type.Equals(RoomType.Operational) ||
                    types[2] && item.Room.Type.Equals(RoomType.PatientCare) ||
                    types[3] && item.Room.Type.Equals(RoomType.Reception) ||
                    types[4] && item.Room.Type.Equals(RoomType.Warehouse))
                { filtered.Add(item); }
            }
            _items = filtered;
        }

        private bool _hasAllTokens(InventoryItem item, string[] tokens)
        {
            foreach (string token in tokens)
            {
                if (!(_containsToken(item.Equipment.Name, token) ||
                    _containsToken(item.Room.Name, token) ||
                    _containsToken(item.Quantity.ToString(), token) ||
                    _containsToken(item.Equipment.TranslateType().ToString(), token) ||
                    _containsToken(item.Room.TranslateType().ToString(), token)))
                { return false; }
            }
            return true;
        }

        private bool _containsToken(string text, string token)
        {
            token = token.Trim();
            return token == "" || text.ToLower().Contains(token);
        }
    }
}
