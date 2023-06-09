using HealthCare.Application.Common;
using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace HealthCare.Model
{
    public class Message : RepositoryItem
    {
        public override object Key
        {
            get => ID;
            set => ID = (int)value;
        }

        public int ID { get; set; }

        public int contactID { get; set; }

        public String senderJMBG { get; set; }

        public String senderName { get; set; }
        public String message { get; set; }
        public DateTime time { get; set; }

        public bool seen { get; set; }
        public override string[] Serialize()
        {
            return new string[] { ID.ToString(), contactID.ToString(), senderJMBG, message, Util.ToString(time), seen.ToString() };
        }

        public override void Deserialize(string[] values)
        {
            ID = int.Parse(values[0]);
            contactID = int.Parse(values[1]);          
            senderJMBG = values[2];
            message = values[3];
            time = Util.ParseDate(values[4]);
            seen = Convert.ToBoolean(values[5]);
        }
    }
}
