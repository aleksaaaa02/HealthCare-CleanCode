using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace HealthCare.GUI.DoctorGUI.AppointmentSchedule
{
    public partial class MakeAppointmentView : Window
    {
        public MakeAppointmentView(DoctorMainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = new MakeAppointmentViewModel(viewModel, this);
        }

        public MakeAppointmentView(DoctorMainViewModel viewModel, Model.Appointment appointment)
        {
            InitializeComponent();
            DataContext = new MakeAppointmentViewModel(appointment, viewModel, this);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}