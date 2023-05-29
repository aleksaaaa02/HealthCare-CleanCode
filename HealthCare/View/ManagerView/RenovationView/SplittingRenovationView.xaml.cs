using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service.RenovationService;
using System.Windows;

namespace HealthCare.View.ManagerView.RenovationView
{
    public partial class SplittingRenovationView : Window
    {
        private readonly JoiningRenovationService _joiningRenovationService;
        public SplittingRenovationView(int roomId, TimeSlot scheduled)
        {
            InitializeComponent();
            
            _joiningRenovationService = Injector.GetService<JoiningRenovationService>();
        }
    }
}
