using HealthCare.Context;
using HealthCare.Model;
using System.Windows;

namespace HealthCare.View.ManagerView.RenovationView
{
    public partial class SplittingRenovationView : Window
    {
        private readonly Hospital _hospital;
        public SplittingRenovationView(Hospital hospital, int roomId, TimeSlot scheduled)
        {
            InitializeComponent();
            
            _hospital = hospital;
        }
    }
}
