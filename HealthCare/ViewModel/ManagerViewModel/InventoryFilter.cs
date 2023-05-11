using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void FilterQuantity(params bool[] quantities)
        {
            if (!(quantities[0] || quantities[1] || quantities[2])) return;

            List<InventoryItemViewModel> filtered = new List<InventoryItemViewModel>();
            foreach (var item in _items)
            {
                if (quantities[0] && item.Quantity == 0 ||
                    quantities[1] && item.Quantity <= 10 ||
                    quantities[2] && item.Quantity > 10)
                { filtered.Add(item); }
            }
            _items = filtered;
        }

        public void FilterEquipmentType(params bool[] types)
        {
            if (!(types[0] || types[1] || types[2] || types[3])) return;

            List<InventoryItemViewModel> filtered = new List<InventoryItemViewModel>();
            foreach (var item in _items)
            {;
                if (types[0] && item.Equipment.Type.Equals(EquipmentType.Examinational) ||
                    types[1] && item.Equipment.Type.Equals(EquipmentType.Operational) ||
                    types[2] && item.Equipment.Type.Equals(EquipmentType.RoomFurniture) ||
                    types[3] && item.Equipment.Type.Equals(EquipmentType.HallwayFurniture))
                { filtered.Add(item); }
            }
            _items = filtered;
        }

        public void FilterRoomType(params bool[] types)
        {
            if (!(types[0] || types[1] || types[2] || types[3] || types[4])) return;

            List<InventoryItemViewModel> filtered = new List<InventoryItemViewModel>();
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

        public void FilterAnyProperty(string query)
        {
            if (query == "") return;
            string[] tokens = query.Split(' ');

            List<InventoryItemViewModel> filtered = new List<InventoryItemViewModel>();
            foreach (var item in _items)
            {
                if (HasAllTokens(item, tokens))
                { filtered.Add(item); }
            }
            _items = filtered;
        }

        private bool HasAllTokens(InventoryItemViewModel item, string[] tokens)
        {
            foreach (string token in tokens)
            {
                if (!(ContainsToken(item.Equipment.Name, token) ||
                    ContainsToken(item.Room.Name, token) ||
                    ContainsToken(item.Quantity.ToString(), token) ||
                    ContainsToken(Utility.Translate(item.Equipment.Type), token) ||
                    ContainsToken(Utility.Translate(item.Room.Type), token)) ||
                    ContainsToken(Utility.Translate(item.Equipment.IsDynamic), token))
                { return false; }
            }
            return true;
        }

        private bool ContainsToken(string text, string token)
        {
            token = token.Trim();
            return token == "" || text.ToLower().Contains(token);
        }
    }
}
