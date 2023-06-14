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

        public String SenderJMBG { get; set; }
        public String MessageText { get; set; }
        public DateTime Time { get; set; }

        public bool Seen { get; set; }
        public override string[] Serialize()
        {
            return new string[] { ID.ToString(), contactID.ToString(), SenderJMBG, MessageText, Util.ToString(Time), Seen.ToString() };
        }

        public override void Deserialize(string[] values)
        {
            ID = int.Parse(values[0]);
            contactID = int.Parse(values[1]);          
            SenderJMBG = values[2];
            MessageText = values[3];
            Time = Util.ParseDate(values[4]);
            Seen = Convert.ToBoolean(values[5]);
        }
    }
}
