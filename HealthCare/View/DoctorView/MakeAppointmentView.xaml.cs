using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.Appointments;
using HealthCare.ViewModels.DoctorViewModel;

namespace HealthCare.View.DoctorView
{
    public partial class MakeAppointmentView : Window
    {
        public MakeAppointmentView(DoctorMainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = new MakeAppointmentViewModel(viewModel, this);
        }

        public MakeAppointmentView(DoctorMainViewModel viewModel, Appointment appointment)
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