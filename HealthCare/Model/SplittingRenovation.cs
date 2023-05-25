using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    public class SplittingRenovation : Renovation
    {
        public Room ResultRoom1 { get; set; }
        public Room ResultRoom2 { get; set; }

        public SplittingRenovation() : this(0, 0, new TimeSlot(), new Room(), new Room()) { }
        public SplittingRenovation(int id, int roomId, TimeSlot scheduled, Room resultRoom1, Room resultRoom2)
            : base(id, roomId, scheduled)
        {
            ResultRoom1 = resultRoom1;
            ResultRoom2 = resultRoom2;
        }

        public override void FromCSV(string[] values)
        {
            base.FromCSV(Utility.SubArray(values, 0, 3));
            ResultRoom1.FromCSV(values[3].Split('|'));
            ResultRoom2.FromCSV(values[4].Split('|'));
        }

        public override string[] ToCSV()
        {
            string room1 = string.Join('|', ResultRoom1.ToCSV());
            string room2 = string.Join('|', ResultRoom2.ToCSV());
            return base.ToCSV().Concat( new string[] { room1, room2 }).ToArray();
        }
    }
}
