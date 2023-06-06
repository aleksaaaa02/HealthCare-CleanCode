using System.Windows;
using HealthCare.ViewModel.DoctorViewModel.AbsenceRequest;

namespace HealthCare.View.DoctorView.AbsenceRequestView
{
    public partial class AbsenceRequestView : Window
    {
        public AbsenceRequestView()
        {
            InitializeComponent();
            DataContext = new AbsenceRequestViewModel();
        }
    }
}
