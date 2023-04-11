using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HealthCare.Service
{
    public class EquipmentService
    {
        public List<Equipment> Equipment { get; set; }
        private CsvStorage<Equipment> csvStorage;

        public EquipmentService(string filepath)
        {
            Equipment = new List<Equipment>();
            csvStorage = new CsvStorage<Equipment>(filepath);
        }

        public Equipment Get(Equipment equipment)
        {
            Equipment? found = Equipment.Find(x => x == equipment);
            if (found is not null) { return found; }
            else { throw new NonExistingObjectException(); }
        }

        public void Add(Equipment equipment)
        {
            if (Contains(equipment)) { throw new DuplicateObjectException(); }
            Equipment.Add(equipment);
        }

        public void Remove(Equipment equipment)
        {
            if (!Contains(equipment)) { throw new NonExistingObjectException(); }
            Equipment.Remove(equipment);
        }

        public void Update(Equipment equipment)
        {
            Equipment current = Get(equipment);
            current.Id = equipment.Id;
            current.Name = equipment.Name;
            current.Type = equipment.Type;
            current.Dynamic = equipment.Dynamic;
        }

        public bool Contains(Equipment equipment)
        {
            return Equipment.Find(x => x.Id == equipment.Id) is not null;
        }

        public void Load()
        {
            Equipment = csvStorage.Load();
        }

        public void Save()
        {
            csvStorage.Save(Equipment);
        }
    }
}
