using HealthCare.Application.Common;
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

        public override void Deserialize(string[] values)
        {
            base.Deserialize(Util.SubArray(values, 0, 4));
            OtherRoomId = int.Parse(values[4]);
            ResultRoom.Deserialize(values[5].Split('|'));
        }

        public override string[] Serialize()
        {
            string room = string.Join('|', ResultRoom.Serialize());
            return base.Serialize().Concat(new string[] { OtherRoomId.ToString(), room }).ToArray();
        }
    }
}
