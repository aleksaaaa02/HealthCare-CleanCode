using HealthCare.Repository;

namespace HealthCare.Model
{
    public class Renovation : Identifier, ISerializable
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int JoiningRoomId { get; set; }
        public bool IsComplex { get; set; }
        public TimeSlot Scheduled { get; set; }
        public Renovation(): this(0, 0, 0, false, new TimeSlot()) { }
        public Renovation(int id, int roomId, int joiningRoomId, bool isComplex, TimeSlot scheduled)
        {
            Id = id;
            RoomId = roomId;
            JoiningRoomId = joiningRoomId;
            IsComplex = isComplex;
            Scheduled = scheduled;
        }

        public override object Key { get => Id; set => Id = (int)value; }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            RoomId = int.Parse(values[1]);
            JoiningRoomId = int.Parse(values[2]);
            IsComplex = bool.Parse(values[3]);
            Scheduled = TimeSlot.Parse(values[4]);
        }

        public string[] ToCSV()
        {
            return new string[] {
                Id.ToString(), RoomId.ToString(), JoiningRoomId.ToString(),
                IsComplex.ToString(), Scheduled.ToString()
            };
        }
    }
}
