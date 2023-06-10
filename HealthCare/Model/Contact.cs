using HealthCare.Application;
using HealthCare.Repository;
using HealthCare.Serialize;
using HealthCare.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCare.Model
{
    public class Contact : RepositoryItem
    {
        public override object Key
        {
            get => ID;
            set => ID = (int)value;
        }

        private int _id;
        public int ID
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                }
            }
        }
        public List<string> Participants { get; set; }

        public override string[] Serialize()
        {
            string recipients = SerialUtil.ToString(Participants);
            return new string[] { ID.ToString(), recipients };
        }

        public override void Deserialize(string[] values)
        {
            ID = int.Parse(values[0]);
            Participants = values[1].Split("|").ToList();
        }
    }
}
