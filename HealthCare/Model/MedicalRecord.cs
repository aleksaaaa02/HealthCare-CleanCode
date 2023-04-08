using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    internal class MedicalRecord
    {
        public float Height { get; set; }
        public float Weight { get; set; }
        public string[] MedicalHistory;
        public MedicalRecord(float height, float weight, string[] medicalHistory)
        {
            Height = height;
            Weight = weight;
            MedicalHistory = medicalHistory;
        }
    }
}
