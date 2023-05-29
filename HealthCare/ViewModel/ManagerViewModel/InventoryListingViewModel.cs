using HealthCare.Application;
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
        private readonly InventoryService _inventoryService;
        private readonly EquipmentService _equipmentService;
        private readonly RoomService _roomService;
        public ObservableCollection<InventoryItemViewModel> Items { get; }
        public ObservableCollection<bool> BoxSelectionArgs { get; }
        private List<InventoryItemViewModel> _models;

        public InventoryListingViewModel()
        {
            _inventoryService = Injector.GetService<InventoryService>(Injector.EQUIPMENT_INVENTORY_S);
            _equipmentService = Injector.GetService<EquipmentService>();
            _roomService = Injector.GetService<RoomService>();

            Items = new ObservableCollection<InventoryItemViewModel>();
            BoxSelectionArgs = new ObservableCollection<bool>();
            
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

            LoadModels(filter.Items);
        }

        public void LoadAll()
        {
            InitializeBoxCollection();
            SearchQuery = "";
            LoadModels(_models);
        }

        private void LoadModels(List<InventoryItemViewModel> items)
        {
            Items.Clear();
            items.ForEach(item => Items.Add(item));
        }

        private List<InventoryItemViewModel> GetModels()
        {
            return _inventoryService.GetAll().Select(x => 
                new InventoryItemViewModel(
                    x, 
                    _equipmentService.Get(x.ItemId), 
                    _roomService.Get(x.RoomId)
            )).ToList();
        }

        private void CollectionChanged(object? sender, NotifyCollectionChangedEventArgs args)
        {
            Filter();
        }

        private void InitializeBoxCollection()
        {
            BoxSelectionArgs.CollectionChanged -= CollectionChanged;

            BoxSelectionArgs.Clear();
            for (int i = 0; i < 12; i++)
                BoxSelectionArgs.Add(false);

            BoxSelectionArgs.CollectionChanged += CollectionChanged;
        }

        private string _searchQuery = "";
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged();
                Filter();
            }
        }
    }
}