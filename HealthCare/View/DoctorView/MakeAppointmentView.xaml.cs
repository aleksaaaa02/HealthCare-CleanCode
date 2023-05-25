using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.Appoinments;
using HealthCare.ViewModels.DoctorViewModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace HealthCare.View.DoctorView
{
    public partial class MakeAppointmentView : Window
    {
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

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
    }
}
