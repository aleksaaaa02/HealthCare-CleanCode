using System.Windows;
using HealthCare.ViewModel.DoctorViewModel.PatientInformation;

namespace HealthCare.View.DoctorView
{
    public partial class PatientSearchView : Window
    {
        public PatientSearchView()
        {
            InitializeComponent();
            DataContext = new PatientSearchViewModel();
        }
    }
}