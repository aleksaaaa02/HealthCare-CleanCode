using HealthCare.Repository;

namespace HealthCare.Model
{
    public class Renovation : Identifier, ISerializable
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public TimeSlot Scheduled { get; set; }
        public Renovation(): this(0, 0, new TimeSlot()) { }
        public Renovation(int id, int roomId, TimeSlot scheduled)
        {
            Id = id;
            RoomId = roomId;
            Scheduled = scheduled;
        }

        public override object Key { get => Id; set => Id = (int)value; }

        public virtual void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            RoomId = int.Parse(values[1]);
            Scheduled = TimeSlot.Parse(values[2]);
        }

        public virtual string[] ToCSV()
        {
            return new string[] { Id.ToString(), RoomId.ToString(),  Scheduled.ToString() };
        }
    }
}
