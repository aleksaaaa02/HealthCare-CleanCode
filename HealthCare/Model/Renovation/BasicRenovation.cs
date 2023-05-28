using HealthCare.Repository;

namespace HealthCare.Model.Renovation
{
    public class BasicRenovation : Identifier, ISerializable
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public TimeSlot Scheduled { get; set; }
        public bool Executed { get; set; }
        public BasicRenovation() : this(0, 0, new TimeSlot(), false) { }
        public BasicRenovation(int id, int roomId, TimeSlot scheduled, bool executed)
        {
            Id = id;
            RoomId = roomId;
            Scheduled = scheduled;
            Executed = executed;
        }

        public override object Key { get => Id; set => Id = (int)value; }

        public virtual void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            RoomId = int.Parse(values[1]);
            Scheduled = TimeSlot.Parse(values[2]);
            Executed = bool.Parse(values[3]);
        }

        public virtual string[] ToCSV()
        {
            return new string[] { Id.ToString(), RoomId.ToString(), Scheduled.ToString(), Executed.ToString() };
        }
    }
}
