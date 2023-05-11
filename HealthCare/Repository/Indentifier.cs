using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Repository
{
    public abstract class Indentifier
    {
        public abstract object Key { get; set; }

        public override bool Equals(object? obj) {
            return obj is Indentifier item &&
                   EqualityComparer<object>.Default.Equals(Key, item.Key);
        }

        public override int GetHashCode() {
            return Key.GetHashCode();
        }
    }
}
