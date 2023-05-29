using HealthCare.Repository;

namespace HealthCare.Model.Renovation
{
    public class BasicRenovation : RepositoryItem
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public TimeSlot Scheduled { get; set; }
        public bool Executed { get; set; }
        public BasicRenovation() : this(0, new TimeSlot()) { }
        public BasicRenovation(int roomId, TimeSlot scheduled)
        {
            Id = 0;
            RoomId = roomId;
            Scheduled = scheduled;
            Executed = false;
        }

        public override object Key { get => Id; set => Id = (int)value; }

        public override void Deserialize(string[] values)
        {
            Id = int.Parse(values[0]);
            RoomId = int.Parse(values[1]);
            Scheduled = TimeSlot.Parse(values[2]);
            Executed = bool.Parse(values[3]);
        }

        public override string[] Serialize()
        {
            return new string[] { Id.ToString(), RoomId.ToString(), Scheduled.ToString(), Executed.ToString() };
        }
    }
}
