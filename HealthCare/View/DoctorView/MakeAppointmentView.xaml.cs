using HealthCare.Model;
using HealthCare.ViewModels.DoctorViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// <summary>
    /// Interaction logic for MakeAppointmentView.xaml
    /// </summary>
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
    }
}
