using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    public class Notification : ISerializable, IKey
    {
        public int Id { get; set; }
        public List<string> UserJmbgs { get; set; }
        public string Text { get; set; }
        public bool Seen { get; set; }
        public Notification() : this(new List<string>(), "", false) { }
        public Notification(List<string> userJmbgs, string text, bool seen) :
            this(0, userJmbgs, text, seen) { }

        public Notification(int id, List<string> userJmbgs, string text, bool seen)
        {
            Id = id;
            UserJmbgs = userJmbgs;
            Text = text;
            Seen = seen;
        }

        public string[] ToCSV()
        {
            string jmbgs = string.Join("|", UserJmbgs);
            return new string[] { Id.ToString(), jmbgs, Text, Seen.ToString() };
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            UserJmbgs = values[1].Split("|").ToList();
            Text = values[2];
            Seen = bool.Parse(values[3]);
        }

        public object GetKey()
        {
            return Id;
        }

        public void SetKey(object key)
        {
            Id = (int) key;
        }
    }
}
