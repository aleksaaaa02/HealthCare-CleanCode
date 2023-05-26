using HealthCare.Context;
using HealthCare.Service;
using HealthCare.ViewModel.NurseViewModel.DataViewModel;
using System.Collections.ObjectModel;

namespace HealthCare.ViewModel.NurseViewModel
{
    public class MedicationOrderListingViewModel:ViewModelBase
    {
        private readonly MedicationService _medicationService;
        private readonly Inventory _medicationInventory;

        public ObservableCollection<OrderMedicationViewModel> Items { get; }

        public MedicationOrderListingViewModel() {
            _medicationService = (MedicationService)ServiceProvider.services["MedicationService"];
            _medicationInventory = (Inventory)ServiceProvider.services["MedicationInventory"];

            Items = new ObservableCollection<OrderMedicationViewModel>();
            LoadAll();
        }

        public void LoadAll()
        {
            Items.Clear();
            foreach (int id in _medicationInventory.GetLowQuantityEquipment(5))
            {
                var medication = _medicationService.Get(id);
                var quantity = _medicationInventory.GetTotalQuantity(id);
                Items.Add(new OrderMedicationViewModel(medication, quantity));
            }

        }
    }
}
