using HealthCare.Application;
using HealthCare.ViewModel.DoctorViewModel.PatientInformation;
using System.Windows;

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
