using HealthCare.Application.Common;
using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model.Renovation
{
    public class SplittingRenovation : BasicRenovation
    {
        public Room ResultRoom1 { get; set; }
        public Room ResultRoom2 { get; set; }

        public SplittingRenovation() : this(0, new TimeSlot(), new Room(), new Room()) { }
        public SplittingRenovation(int roomId, TimeSlot scheduled, Room resultRoom1, Room resultRoom2)
            : base(roomId, scheduled)
        {
            ResultRoom1 = resultRoom1;
            ResultRoom2 = resultRoom2;
        }

        public override void Deserialize(string[] values)
        {
            base.Deserialize(Util.SubArray(values, 0, 4));
            ResultRoom1.Deserialize(values[4].Split('|'));
            ResultRoom2.Deserialize(values[5].Split('|'));
        }

        public override string[] Serialize()
        {
            string room1 = string.Join('|', ResultRoom1.Serialize());
            string room2 = string.Join('|', ResultRoom2.Serialize());
            return base.Serialize().Concat(new string[] { room1, room2 }).ToArray();
        }
    }
}
