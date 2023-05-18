using HealthCare.Context;
using HealthCare.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Navigation;

namespace HealthCare.ViewModel.ManagerViewModel
{
    public class InventoryListingViewModel : ViewModelBase
    {
        private readonly Inventory _inventory;
        private readonly EquipmentService _equipmentService;
        private readonly RoomService _roomService;
        public ObservableCollection<InventoryItemViewModel> Items { get; }
        private List<InventoryItemViewModel> _models;

        public InventoryListingViewModel(Hospital hospital)
        {
            _inventory = hospital.Inventory;
            _equipmentService = hospital.EquipmentService;
            _roomService = hospital.RoomService;
            Items = new ObservableCollection<InventoryItemViewModel>();
            _tgBoxes = new ObservableCollection<bool>();

            _models = GetModels();
            LoadAll();
        }

        public void Filter()
        {
            InventoryFilter filter = new InventoryFilter(_models);

            var searchParams = TgBoxes.ToArray();

            filter.FilterQuantity(searchParams);
            filter.FilterEquipmentType(searchParams);
            filter.FilterRoomType(searchParams);
            filter.FilterAnyProperty(_tbQuery);
            LoadModels(filter.GetFiltered());
        }

        public void LoadAll()
        {
            ResetElements();
            LoadModels(_models);
        }

        private void LoadModels(List<InventoryItemViewModel> items)
        {
            Items.Clear();
            items.ForEach(item => Items.Add(item));
        }

        private List<InventoryItemViewModel> GetModels()
        {
            var models = new List<InventoryItemViewModel>();
            _inventory.GetAll().ForEach(x => {
                var equipment = _equipmentService.Get(x.EquipmentId);
                var room = _roomService.Get(x.RoomId);
                models.Add(new InventoryItemViewModel(x, equipment, room));
            });
            return models;
        }

        private string _tbQuery = "";
        public string TbQuery
        {
            get => _tbQuery;
            set
            {
                _tbQuery = value;
                OnPropertyChanged();
                Filter();
            }
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Replace) 
                Filter();
        }
        
        private ObservableCollection<bool> _tgBoxes;
        public ObservableCollection<bool> TgBoxes
        {
            get => _tgBoxes;
            set
            {
                _tgBoxes = value;
                OnPropertyChanged();
                Filter();
            }
        }

        private void InitializeBoxCollection()
        {
            TgBoxes.Clear();
            for (int i = 0; i < 12; i++)
                TgBoxes.Add(false);
        }

        private void ResetElements()
        {
            InitializeBoxCollection();
            TbQuery = "";
        }
    }
}
