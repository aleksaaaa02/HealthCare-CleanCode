using HealthCare.Context;
using HealthCare.Serialize;
using HealthCare.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace HealthCare.ViewModel.ManagerViewModel
{
    public class InventoryListingViewModel : ViewModelBase
    {
        private readonly Inventory _inventory;
        private readonly EquipmentService _equipmentService;
        private readonly RoomService _roomService;
        public ObservableCollection<InventoryItemViewModel> Items { get; }
        public ObservableCollection<bool> BoxSelectionArgs { get; }
        private List<InventoryItemViewModel> _models;

        public InventoryListingViewModel(Hospital hospital)
        {
            _inventory = hospital.Inventory;
            _equipmentService = hospital.EquipmentService;
            _roomService = hospital.RoomService;
            Items = new ObservableCollection<InventoryItemViewModel>();
            BoxSelectionArgs = new ObservableCollection<bool>();
            BoxSelectionArgs.CollectionChanged += CollectionChanged;

            _models = GetModels();
            LoadAll();
        }

        public void Filter()
        {
            InventoryFilter filter = new InventoryFilter(_models);

            var args = BoxSelectionArgs.ToArray();
            var quantityArgs = Utility.SubArray(args, 0, 3);
            var equipmentArgs = Utility.SubArray(args, 3, 4);
            var roomArgs = Utility.SubArray(args, 7, 5);

            filter.FilterQuantity(quantityArgs);
            filter.FilterEquipmentType(equipmentArgs);
            filter.FilterRoomType(roomArgs);
            filter.FilterAnyProperty(_searchQuery);

            LoadModels(filter.GetFiltered());
        }

        public void LoadAll()
        {
            SearchQuery = "";
            InitializeBoxCollection();
            LoadModels(_models);
        }

        private void LoadModels(List<InventoryItemViewModel> items)
        {
            Items.Clear();
            items.ForEach(item => Items.Add(item));
        }

        private List<InventoryItemViewModel> GetModels()
        {
            return _inventory.GetAll().Select(x => 
                new InventoryItemViewModel(
                    x, 
                    _equipmentService.Get(x.EquipmentId), 
                    _roomService.Get(x.RoomId)
            )).ToList();
        }

        private void CollectionChanged(object? sender, NotifyCollectionChangedEventArgs args)
        {
            var collection = sender as ObservableCollection<bool>;
            if (collection != null && collection.Count == 12)
                Filter();
        }

        private void InitializeBoxCollection()
        {
            BoxSelectionArgs.Clear();
            for (int i = 0; i < 12; i++)
                BoxSelectionArgs.Add(false);
        }

        private string _searchQuery = "";
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged();
                if (value != "") Filter();
            }
        }
    }
}