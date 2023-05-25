using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    public class JoiningRenovation : Renovation
    {
        public int OtherRoomId { get; set; }
        public Room ResultRoom { get; set; }

        public JoiningRenovation() : this(0, 0, new TimeSlot(), 0, new Room()) { }
        public JoiningRenovation(int id, int roomId, TimeSlot scheduled, int otherRoomId, Room resultRoom)
            : base(id, roomId, scheduled)
        {
            OtherRoomId = otherRoomId;
            ResultRoom = resultRoom;
        }

        public override void FromCSV(string[] values)
        {
            base.FromCSV(Utility.SubArray(values, 0, 3));
            OtherRoomId = int.Parse(values[3]);
            ResultRoom.FromCSV(values[4].Split('|'));
        }

        public override string[] ToCSV()
        {
            string room = string.Join('|', ResultRoom.ToCSV());
            return base.ToCSV().Concat(new string[] { OtherRoomId.ToString(), room }).ToArray();
        }
    }
}
