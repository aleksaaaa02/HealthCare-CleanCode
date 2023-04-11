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
        private Hospital _hospital;

        public ObservableCollection<InventoryItemViewModel> Items;

        public ManagerMainViewModel(Hospital hospital)
        {
            _hospital = hospital;
            Items = new ObservableCollection<InventoryItemViewModel>();
            Update();
        }

        public void Update()
        {
            Items.Clear();
            foreach (var item in _hospital.Inventory.Items)
            {
                Items.Add(new InventoryItemViewModel(item));
            }
        }
    }
}
