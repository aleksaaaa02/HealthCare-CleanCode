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

        public InventoryListingViewModel()
        {
            InventoryItems = new ObservableCollection<InventoryItemViewModel>();
        }

        public void UpdateItems(List<InventoryItem> items)
        {
            InventoryItems.Clear();
            foreach(var item in items)
            {
                InventoryItemViewModel model = new InventoryItemViewModel(item);
                InventoryItems.Add(model);
            }
        }
    }
}
