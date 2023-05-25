﻿using HealthCare.Model;
using HealthCare.View;
using System.Windows.Media;

namespace HealthCare.ViewModel.NurseViewModel.DataViewModel
{
    public class PrescriptionViewModel
    {
        public Prescription Prescription { get; set; }
        public string MedicationName { get; set; }
        public bool IsLow { get; set; }
        public string MealTime => Utility.Translate(Prescription.Instruction);
        public string DoctorName { get; set; }
        public string Date { get; set; }
        public string FirstUse => Utility.Translate(Prescription.FirstUse);
        public string Empty => Utility.Translate(!IsLow);
        public Brush Color => IsLow ? Brushes.Red : Brushes.Black;
        public PrescriptionViewModel(Prescription prescription, int Quantity, string medicationName,string doctor)
        {
            Prescription = prescription;
            IsLow = Quantity == 0;
            MedicationName = medicationName;
            DoctorName = doctor;
            Date = prescription.Start.ToString().Split(" ")[0];
        }
    }
}
