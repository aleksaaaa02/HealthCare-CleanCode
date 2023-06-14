using System.Windows;
using HealthCare.ViewModel.ManagerViewModel;

namespace HealthCare.View.ManagerView
{
    public partial class AbsenceRequestListingView : Window
    {
        public AbsenceRequestListingView()
        {
            InitializeComponent();

            DataContext = new AbsenceRequestListingViewModel(this);
        }
    }
}