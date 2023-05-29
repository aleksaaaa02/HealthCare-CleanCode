using HealthCare.Repository;
using HealthCare.Serialize;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Model
{
    public class Medication : RepositoryItem
    {
        public override object Key { get => Id; set { Id = (int)value; } }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Ingredients { get; set; }
        public Medication() { }

        public Medication(string name, string description, List<string> ingredients)
        {
            Name = name;
            Description = description;
            Ingredients = ingredients;
        }

        public override string[] Serialize()
        {
            return new string[] { Id.ToString(), Name, Description, SerialUtil.ToString(Ingredients) };
        }

        public override void Deserialize(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Description = values[2];
            Ingredients = values[3].Split("|").ToList();

        }
    }
}