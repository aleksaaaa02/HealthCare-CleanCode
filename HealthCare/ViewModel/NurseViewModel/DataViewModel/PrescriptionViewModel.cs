using HealthCare.Model;

namespace HealthCare.ViewModel.NurseViewModel.DataViewModel
{
    public class PrescriptionViewModel
    {
        public Prescription Prescription { get; set; }
        public bool IsLow { get; set; }
        public PrescriptionViewModel(Prescription prescription, int Quantity)
        {
            Prescription = prescription;
            IsLow = Quantity < 5;
        }
    }
}
