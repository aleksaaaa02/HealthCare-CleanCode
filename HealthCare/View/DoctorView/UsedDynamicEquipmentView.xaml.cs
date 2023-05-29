using HealthCare.Application;
using HealthCare.ViewModel.DoctorViewModel.UsedEquipment;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace HealthCare.View.DoctorView
{
    public partial class UsedDynamicEquipmentView : Window
    {
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        public UsedDynamicEquipmentView(int roomId)
        {
            InitializeComponent();
            DataContext = new UsedDynamicEquipmentViewModel(this, roomId);
        }

    
    }
}
