using HealthCare.Context;
using HealthCare.Service;
using HealthCare.ViewModel.NurseViewModel.DataViewModel;
using System.Collections.ObjectModel;

namespace HealthCare.ViewModel.NurseViewModel
{
    public class MedicationOrderListingViewModel:ViewModelBase
    {
        private readonly Inventory _inventory;
        private readonly MedicationService _medicationService;

        public ObservableCollection<OrderMedicationViewModel> Items { get; }

        public MedicationOrderListingViewModel(Hospital hospital) {
            _inventory = hospital.MedicationInventory;
            _medicationService = hospital.MedicationService;
            Items = new ObservableCollection<OrderMedicationViewModel>();

            LoadAll();
        }

        public void LoadAll()
        {
            Items.Clear();
            foreach (int id in _inventory.GetLowQuantityEquipment(5))
            {
                var medication = _medicationService.Get(id);
                var quantity = _inventory.GetTotalQuantity(id);
                Items.Add(new OrderMedicationViewModel(medication, quantity));
            }

        }
    }
}
