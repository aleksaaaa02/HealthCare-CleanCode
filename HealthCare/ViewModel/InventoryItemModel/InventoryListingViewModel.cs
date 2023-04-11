using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.ViewModel.InventoryItemModel
{
    public class InventoryListingViewModel
    {
        private readonly ObservableCollection<InventoryItemViewModel> _inventoryItems;

        public InventoryListingViewModel(List<InventoryItem> items)
        {
            _inventoryItems = new ObservableCollection<InventoryItemViewModel>();

            // TODO
        }
    }
}
