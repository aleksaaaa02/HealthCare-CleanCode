using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace HealthCare.View.AppointmentView
{
    /// <summary>
    /// Interaction logic for PatientRecordView.xaml
    /// </summary>
    public partial class PatientRecordView : Window
    {
        Hospital _hospital;
        public void LoadData()
        {
            List<Appointment> appointments = Schedule.GetPatientAppointments((Patient)_hospital.Current);
            listView1.ItemsSource = new ObservableCollection<Appointment>(appointments);
        }

        public PatientRecordView(Hospital hospital)
        {
            _hospital = hospital;
            InitializeComponent();
            LoadData();
        }
    }
}
