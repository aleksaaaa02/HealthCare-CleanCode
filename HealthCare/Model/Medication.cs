using HealthCare.Repository;

namespace HealthCare.Model
{
    public class Medication : Equipment, ISerializable
    {
        public string Description { get; set; }
        public string[] Ingredients { get; set; }
        public Medication() { }
        
        public Medication(int id, string name, string description, string[] ingredients) : base(id,name,EquipmentType.Medication,true)
        {
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
