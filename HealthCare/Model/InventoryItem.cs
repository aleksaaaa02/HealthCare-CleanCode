using HealthCare.Repository;
using HealthCare.Serialize;

namespace HealthCare.Model
{
    public class InventoryItem : RepositoryItem
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public int RoomId { get; set; }
        public int Quantity { get; set; }

        public InventoryItem() : this(0) { }
        public InventoryItem(int id) : this(id, 0, 0, 0) { }
        public InventoryItem(int equipmentId, int roomId, int quantity) :
            this(0, equipmentId, roomId, quantity) { }
        public InventoryItem(int id, int equipmentId, int roomId, int quantity)
        {
            Id = id;
            EquipmentId = equipmentId;
            RoomId = roomId;
            Quantity = quantity;
        }

        public override object Key
        {
            get => Id;
            set { Id = (int)value; }
        }

        public override string[] Serialize()
        {
            return new string[] {
                Id.ToString(),
                EquipmentId.ToString(),
                RoomId.ToString(),
                Quantity.ToString()};
        }

        public override void Deserialize(string[] values)
        {
            Id = int.Parse(values[0]);
            EquipmentId = int.Parse(values[1]);
            RoomId = int.Parse(values[2]);
            Quantity = int.Parse(values[3]);
        }
    }
}
