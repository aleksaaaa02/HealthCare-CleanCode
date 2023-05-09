using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Diagnostics;
using System.Windows.Input;

namespace HealthCare.Service
{
    public class TransferService : NumericService<TransferItem>
    {
        private readonly Inventory _inventory;

        public TransferService(string filepath, Inventory inventory) : base(filepath)
        {
            _inventory = inventory;
        }

        public bool Execute(TransferItem transfer) {
            InventoryItem reduceItem = new InventoryItem(
                transfer.EquipmentId, transfer.FromRoom, transfer.Quantity);
            InventoryItem restockItem = new InventoryItem(
                transfer.EquipmentId, transfer.ToRoom, transfer.Quantity);

            if (!_inventory.TryReduceInventoryItem(reduceItem))
                return false;

            _inventory.RestockInventoryItem(restockItem);
            transfer.Executed = true;

            if (!Contains(transfer.Id)) 
                Add(transfer);
            else Update(transfer);
            return true;
        }

        public int ExecuteTransfers()
        {
            int failed = 0;
            GetAll().ForEach(x => {
                if (!x.Executed && x.Scheduled <= DateTime.Now && !Execute(x))
                    ++failed;
            });
            return failed;
        }
    }
}
