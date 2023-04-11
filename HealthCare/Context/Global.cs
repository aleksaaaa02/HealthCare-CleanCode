using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Context
{
    static class Global
    {
        public const string dirPath = "../../../Resource/";
        public const string roomPath = dirPath + "rooms.csv";
        public const string nursePath = dirPath + "nurses.csv";
        public const string doctorPath = dirPath + "doctors.csv";
        public const string patientPath = dirPath + "patients.csv";
        public const string equipmentPath = dirPath + "equipment.csv";
        public const string appointmentPath = dirPath + "appointments.csv";
        public const string inventoryItemPath = dirPath + "inventory_items.csv";

        public const string managerUsername = "admin";
        public const string managerPassword = "admin";
    }
}
