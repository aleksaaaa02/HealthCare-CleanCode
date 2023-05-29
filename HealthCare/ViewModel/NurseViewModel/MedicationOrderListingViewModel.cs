using HealthCare.Application;
using HealthCare.Service;
using HealthCare.ViewModel.NurseViewModel.DataViewModel;
using System.Collections.ObjectModel;

namespace HealthCare.ViewModel.NurseViewModel
{
    public class MedicationOrderListingViewModel:ViewModelBase
    {
        private readonly MedicationService _medicationService;
        private readonly InventoryService _inventoryService;

        public ObservableCollection<OrderMedicationViewModel> Items { get; }

        public MedicationOrderListingViewModel() {
            _medicationService = Injector.GetService<MedicationService>();
            _inventoryService = Injector.GetService<InventoryService>(Injector.MEDICATION_INVENTORY_S);

            Items = new ObservableCollection<OrderMedicationViewModel>();
            LoadAll();
        }

        public void LoadAll()
        {
            Items.Clear();
            foreach (int id in _inventoryService.GetLowQuantityEquipment(5))
            {
                var medication = _medicationService.Get(id);
                var quantity = _inventoryService.GetTotalQuantity(id);
                Items.Add(new OrderMedicationViewModel(medication, quantity));
            }

        }
    }
}
