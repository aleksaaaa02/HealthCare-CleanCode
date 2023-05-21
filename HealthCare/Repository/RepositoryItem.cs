using HealthCare.Serialize;
using System.Collections.Generic;

namespace HealthCare.Repository
{
    public abstract class RepositoryItem : ISerializable
    {
        public abstract object Key { get; set; }


        public override bool Equals(object? obj)
        {
            return obj is RepositoryItem other && Key.Equals(other.Key);
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }

        public abstract string[] Serialize();
        public abstract void Deserialize(string[] values);
    }
}