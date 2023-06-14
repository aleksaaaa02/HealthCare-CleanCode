using System.Windows;
using HealthCare.Model;

namespace HealthCare.GUI.DoctorGUI.Healthcare.PatientTreatment
{
    public partial class DoctorTreatmentVisitView : Window
    {
        public DoctorTreatmentVisitView(Treatment treatment)
        {
            InitializeComponent();
            DataContext = new DoctorTreatmentVisitViewModel(this, treatment);
        }
    }
}