using HealthCare.Repository;
using System.Linq;

namespace HealthCare.Model.Renovation
{
    public class JoiningRenovation : BasicRenovation
    {
        public int OtherRoomId { get; set; }
        public Room ResultRoom { get; set; }

        public JoiningRenovation() : this(0, new TimeSlot(), 0, new Room()) { }
        public JoiningRenovation(int roomId, TimeSlot scheduled, int otherRoomId, Room resultRoom)
            : base(roomId, scheduled)
        {
            OtherRoomId = otherRoomId;
            ResultRoom = resultRoom;
        }

        public override void FromCSV(string[] values)
        {
            base.FromCSV(values.SubArray(0, 4));
            OtherRoomId = int.Parse(values[4]);
            ResultRoom.FromCSV(values[5].Split('|'));
        }

        public override string[] ToCSV()
        {
            string room = string.Join('|', ResultRoom.ToCSV());
            return base.ToCSV().Concat(new string[] { OtherRoomId.ToString(), room }).ToArray();
        }
    }
}
