using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModels.DoctorViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HealthCare.View.DoctorView
{


    public partial class MakeAppointmentView : Window
    {
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public MakeAppointmentView(Hospital hospital, DoctorMainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = new MakeAppointmentViewModel(hospital ,viewModel, this);
        }
        public MakeAppointmentView(Hospital hospital, DoctorMainViewModel viewModel, Appointment appointment)
        {
            InitializeComponent();
            DataContext = new MakeAppointmentViewModel(hospital, appointment, viewModel, this);
        }
    }
}
