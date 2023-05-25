using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Repository;
using System;

namespace HealthCare.Service
{
    public class TransferService : NumericService<TransferItem>
    {
        private readonly Inventory _inventory;

        public TransferService(string filepath) : base(filepath)
        {
            _inventory = (Inventory)ServiceProvider.services["EquipmentInventory"];
        }

        private TransferService(IRepository<TransferItem> repository) : base(repository) 
        {
            _inventory = (Inventory)ServiceProvider.services["EquipmentInventory"];
        }

        private static TransferService? _instance = null;
        public static TransferService GetInstance(IRepository<TransferItem> repository)
        {
            if (_instance is not null) return _instance;
            _instance = new TransferService(repository);
            return _instance;
        }

        public void Execute(TransferItem transfer) {
            var reduceItem = new InventoryItem(
                transfer.EquipmentId, transfer.FromRoom, transfer.Quantity);
            var restockItem = new InventoryItem(
                transfer.EquipmentId, transfer.ToRoom, transfer.Quantity);

            if (!_inventory.TryReduceInventoryItem(reduceItem))
                return;

            _inventory.RestockInventoryItem(restockItem);
            transfer.Executed = true;

            if (transfer.Id == 0) 
                Add(transfer);
            else 
                Update(transfer);
        }

        public void ExecuteAll()
        {
            GetAll().ForEach(x => {
                if (!x.Executed && x.Scheduled <= DateTime.Now)
                    Execute(x);
            });
        }
    }
}
