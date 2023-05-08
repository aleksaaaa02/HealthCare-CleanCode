using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel;
using HealthCare.ViewModel.DoctorViewModel.Examination;
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
    /// Interaction logic for DoctorExamView.xaml
    /// </summary>
    public partial class DoctorExamView : Window
    {
        public DoctorExamView(Hospital hospital, Appointment appointment, int roomId)
        {
            InitializeComponent();
            DataContext = new DoctorExamViewModel(hospital, this, appointment, roomId);
        }
    }
}
