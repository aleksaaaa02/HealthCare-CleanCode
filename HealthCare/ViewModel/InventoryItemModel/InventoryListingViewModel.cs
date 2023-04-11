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
        private readonly ObservableCollection<InventoryItemViewModel> InventoryItems;

        public InventoryListingViewModel(List<InventoryItem> items)
        {
            InventoryItems = new ObservableCollection<InventoryItemViewModel>();

            // TODO
        }
    }
}
