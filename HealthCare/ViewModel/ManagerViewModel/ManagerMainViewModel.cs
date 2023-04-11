using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModels.DoctorViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthCare.ViewModel.ManagerViewModel
{
    public class ManagerMainViewModel : ViewModelBase
    {
        public readonly Hospital Hospital;
        public ObservableCollection<InventoryItemViewModel> Items { get; set; }
        private List<InventoryItem> _filtered;

        public ManagerMainViewModel(Hospital hospital)
        {
            Hospital = hospital;
            Items = new ObservableCollection<InventoryItemViewModel>();
            _filtered = new List<InventoryItem>();
            LoadAll();
        }

        public void LoadAll()
        {
            _load(Hospital.Inventory.Items);
        }

        private void _load(List<InventoryItem> items)
        {
            Items.Clear();
            foreach (var item in items)
            {
                _add(item);
            }
        }

        public void Filter(string query, bool[] quantities, bool[] equipmentTypes, bool[] roomTypes)
        {
            _filtered = new List<InventoryItem>();
            _filtered.AddRange(Hospital.Inventory.Items);
            FilterAnyProperty(query);
            FilterQuantity(quantities);
            FilterEquipmentType(equipmentTypes);
            FilterRoomType(roomTypes);
            _load(_filtered);
        }

        private void _add(InventoryItem item)
        {
            Items.Add(new InventoryItemViewModel(item));
        }

        private bool _containsToken(string text, string[] tokens)
        {
            foreach (var token in tokens)
                if (text.ToLower().Contains(token)) return true;
            return false;
        }

        public void FilterAnyProperty(string query)
        {
            if (query == "") return;
            string[] tokens = query.Split(' ');

            List<InventoryItem> toFilter = new List<InventoryItem>();
            foreach (var item in _filtered)
            {
                if (_containsToken(item.Equipment.Name, tokens)) toFilter.Add(item);
                else if (_containsToken(item.Room.Name, tokens)) toFilter.Add(item);
                else if (_containsToken(item.Quantity.ToString(), tokens)) toFilter.Add(item);
                // else if (_containsToken(item.Equipment.TranslateType().ToString(), tokens) toFilter.Add(item);
                // else if (_containsToken(item.Room.TranslateType().ToString(), tokens) toFilter.Add(item);
            }
            _filtered.Clear();
            _filtered.AddRange(toFilter);
        }

        public void FilterQuantity(bool[] quantities)
        {
            if (!quantities[0] && !quantities[1] && !quantities[2]) return;

            List<InventoryItem> toFilter = new List<InventoryItem>();
            foreach (var item in _filtered)
            {
                if (quantities[0] && item.Quantity == 0) toFilter.Add(item);
                else if (quantities[1] && item.Quantity <= 10) toFilter.Add(item);
                else if (quantities[2] && item.Quantity > 10) toFilter.Add(item);
            }
            _filtered.Clear();
            _filtered.AddRange(toFilter);
        }

        public void FilterEquipmentType(bool[] types)
        {
            if (!types[0] && !types[1] && !types[2] && !types[3]) return;

            List<InventoryItem> toFilter = new List<InventoryItem>();
            foreach (var item in _filtered)
            {
                if (types[0] && item.Equipment.Type.Equals(EquipmentType.Examinational))
                    toFilter.Add(item);
                else if (types[1] && item.Equipment.Type.Equals(EquipmentType.Operational))
                    toFilter.Add(item);
                else if (types[2] && item.Equipment.Type.Equals(EquipmentType.RoomFurniture))
                    toFilter.Add(item);
                else if (types[3] && item.Equipment.Type.Equals(EquipmentType.HallwayFurniture))
                    toFilter.Add(item);
            }
            _filtered.Clear();
            _filtered.AddRange(toFilter);
        }

        public void FilterRoomType(bool[] types)
        {
            if (!types[0] && !types[1] && !types[2] && !types[3] && !types[4]) return;

            List<InventoryItem> toFilter = new List<InventoryItem>();
            foreach (var item in _filtered)
            {
                if (types[0] && item.Room.Type.Equals(RoomType.Examinational))
                    toFilter.Add(item);
                else if (types[1] && item.Room.Type.Equals(RoomType.Operational))
                    toFilter.Add(item);
                else if (types[2] && item.Room.Type.Equals(RoomType.PatientCare))
                    toFilter.Add(item);
                else if (types[3] && item.Room.Type.Equals(RoomType.Reception))
                    toFilter.Add(item);
                else if (types[4] && item.Room.Type.Equals(RoomType.Warehouse))
                    toFilter.Add(item);
            }
            _filtered.Clear();
            _filtered.AddRange(toFilter);
        }
    }
}
