using HealthCare.Service;
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

namespace HealthCare.View.PatientView
{
    /// <summary>
    /// Interaction logic for NurseMainView.xaml
    /// </summary>
    public partial class NurseMainView : Window
    {
        public NurseMainView()
        {
            InitializeComponent();
            PatientService patientService = new PatientService("..\\..\\..\\da.csv");
            patientService.Load();
            lvPatients.ItemsSource = patientService.Patients;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
