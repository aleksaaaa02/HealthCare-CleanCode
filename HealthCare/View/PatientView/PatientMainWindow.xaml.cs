using HealthCare.Context;
using HealthCare.View.AppointmentView;
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
    /// Interaction logic for PatientMainWindow.xaml
    /// </summary>
    public partial class PatientMainWindow : Window
    {
        Hospital _hospital;
        public PatientMainWindow(Hospital hospital)
        {
            InitializeComponent();
            _hospital = hospital;
            //#179c8c green
            //#effcfa white
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainContentGrid.Content = new PatientRecordView(_hospital);
        }
    }
}
