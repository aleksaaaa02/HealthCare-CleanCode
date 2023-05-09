using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    public class Notification : Indentifier, ISerializable
    {
        public override object Key { get => Id; set => Id = (int)value; }
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Seen { get; set; }
        public string[] UserJmbgs { get; set; }
        public Notification() : this("", new string[0]) { }
        public Notification(string text, params string[] userJmbgs) :
            this(0, text, false, userJmbgs)
        { }

        public Notification(int id, string text, bool seen, params string[] userJmbgs)
        {
            Id = id;
            Text = text;
            Seen = seen;
            UserJmbgs = userJmbgs;
        }

        public string Display()
        {
            Seen = true;
            return Text;
        }

        public string[] ToCSV()
        {
            string jmbgs = Utility.ToString(UserJmbgs);
            return new string[] { Id.ToString(), jmbgs, Text, Seen.ToString() };
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            UserJmbgs = values[1].Split("|");
            Text = values[2];
            Seen = bool.Parse(values[3]);
        }
    }
}
