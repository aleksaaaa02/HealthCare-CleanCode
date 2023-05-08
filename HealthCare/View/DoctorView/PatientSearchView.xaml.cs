using HealthCare.Context;
using HealthCare.ViewModel.DoctorViewModel;
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
    /// Interaction logic for PatientSearchView.xaml
    /// </summary>
    public partial class PatientSearchView : Window
    {
        public PatientSearchView(Hospital hospital)
        {
            InitializeComponent();
            DataContext = new PatientSearchViewModel(hospital);

        }
    }
}
