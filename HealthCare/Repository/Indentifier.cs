using System.Collections.Generic;

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
