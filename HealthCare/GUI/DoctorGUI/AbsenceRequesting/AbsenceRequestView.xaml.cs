using System.Windows;

namespace HealthCare.GUI.DoctorGUI.AbsenceRequesting
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