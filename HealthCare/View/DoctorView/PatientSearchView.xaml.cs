using HealthCare.Context;
using HealthCare.ViewModel.DoctorViewModel.PatientInformation;
using System.Windows;

namespace HealthCare.View.DoctorView
{
    /// <summary>
    /// Interaction logic for PatientSearchView.xaml
    /// </summary>
    public partial class PatientSearchView : Window
    {
        public PatientSearchView(Hospital hospital)
        {
            InitializeComponent();
            DataContext = new PatientSearchViewModel(hospital);

        }
    }
}
