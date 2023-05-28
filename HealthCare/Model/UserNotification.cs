using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HealthCare.Model
{
    public class UserNotification : Identifier, ISerializable
    {
        public override object Key { get => notificationID; set => notificationID = (int)value; }
        public int notificationID { get; set; }
        public string patientID { get; set; }

        public DateTime receiveTime { get; set; }

        public String caption { get; set; }

        public String text { get; set; }

        public bool isCustom { get; set; }

        public UserNotification() : this("", DateTime.Now, "", "", false) { }
        public UserNotification(string patientID, DateTime receiveTime, String caption, String text, bool isCustom) 
        { 
            this.patientID = patientID;
            this.receiveTime = receiveTime;
            this.caption = caption;
            this.text = text;
            this.isCustom = isCustom;
        }

        public string[] ToCSV()
        {
            return new string[] { notificationID.ToString(), patientID.ToString(), Utility.ToString(receiveTime), caption, text};
        }

        public void FromCSV(string[] values)
        {
            notificationID = int.Parse(values[0]);
            patientID = values[1].ToString();
            receiveTime = Utility.ParseDate(values[2]);
            caption = values[3];
            text = values[4];
            isCustom = true;
        }


    }
}
