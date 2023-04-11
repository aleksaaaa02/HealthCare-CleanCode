using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public Equipment Get(string equipmentName)
        {
            Equipment? found = Equipment.Find(x => x.Name == equipmentName);
            if (found != null) { return found; }
            throw new ObjectNotFoundException();
        }

        public void Add(Equipment equipment)
        {
            if (Contains(equipment.Name)) throw new ObjectAlreadyExistException();
            Equipment.Add(equipment);
        }

        public void Remove(string equipmentName)
        {
            if (!Contains(equipmentName)) throw new ObjectNotFoundException();
            Equipment.RemoveAll(x => x.Name == equipmentName);
        }

        public void Update(Equipment equipment)
        {
            if (!Contains(equipment.Name)) throw new ObjectNotFoundException();
            Equipment current = Get(equipment.Name);
            current.Copy(equipment);
        }

        public bool Contains(string equipmentName)
        {
            return Equipment.FindIndex(x => x.Name == equipmentName) >= 0;
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
