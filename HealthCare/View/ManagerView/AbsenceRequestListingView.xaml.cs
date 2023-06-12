using HealthCare.ViewModel.ManagerViewModel;
using System.Windows;

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
