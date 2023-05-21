using HealthCare.Repository;

namespace HealthCare.Model
{
    public class Medication : Identifier, ISerializable
    {
        public override object Key { get => Id; set { Id = (int) value; } }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] Ingredients { get; set; }
        public Medication() { }
        
        public Medication(string name, string description, string[] ingredients)
        {
            Name = name;
            Description = description;
            Ingredients = ingredients;
        }

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), Name, Description,  Utility.ToString(Ingredients)};
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Description = values[2];
            Ingredients = values[3].Split("|");

        }
    }
}
