using System.Windows;
using HealthCare.ViewModel.DoctorViewModel.AbsenceRequests;

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
