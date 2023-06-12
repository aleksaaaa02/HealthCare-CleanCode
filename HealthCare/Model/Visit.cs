using HealthCare.Application.Common;
using HealthCare.Repository;
using System;

namespace HealthCare.Model
{
    public class Visit : RepositoryItem
    {
        public int Id { get; set; }
        public double Temperature { get; set; } 
        public int SystolicPressure { get; set; }
        public int DiastolicPressure { get; set; }
        public string Observations { get; set; }
        public DateTime VisitTime { get; set; }
        public int TreatmentId { get; set; }
        
        public override object Key { get => Id; set => Id = (int) value; }

        public override void Deserialize(string[] values)
        {
            Id = int.Parse(values[0]);
            Temperature = double.Parse(values[1]);
            SystolicPressure = int.Parse(values[2]);
            DiastolicPressure = int.Parse(values[3]);
            Observations = values[4];
            VisitTime = Util.ParseDate(values[5]);
            TreatmentId = int.Parse(values[6]);
        }

        public override string[] Serialize()
        {
            return new[] { Id.ToString(), Temperature.ToString(), SystolicPressure.ToString(),
                DiastolicPressure.ToString(),Observations.ToString(),
                Util.ToString(VisitTime),TreatmentId.ToString()};
        }

        public bool isMorningVisit() {
            return DateTime.Now < DateTime.Now.Date.AddHours(12);
        }
    }
}
