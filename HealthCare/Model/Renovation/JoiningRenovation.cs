using HealthCare.Repository;
using System.Linq;

namespace HealthCare.Model.Renovation
{
    public class JoiningRenovation : Renovation
    {
        public int OtherRoomId { get; set; }
        public Room ResultRoom { get; set; }

        public JoiningRenovation() : this(0, 0, new TimeSlot(), false, 0, new Room()) { }
        public JoiningRenovation(int id, int roomId, TimeSlot scheduled, bool executed, int otherRoomId, Room resultRoom)
            : base(id, roomId, scheduled, executed)
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
