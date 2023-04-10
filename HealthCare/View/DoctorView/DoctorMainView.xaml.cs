using HealthCare.Model;
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

namespace HealthCare.View.DoctorView
{
    /// <summary>
    /// Interaction logic for DoctorMainView.xaml
    /// </summary>
    public partial class DoctorMainView : Window
    {
        
        public DoctorMainView()
        {
           
            InitializeComponent();
            List<Appointment> appointments = new List<Appointment>();
            Doctor doc = new Doctor("Aleksa", "Vukomanovic", "123456789", DateTime.Now, "062173224", "Vuka Karadzica", "aleksa123", "radi", Gender.Male, "Hirurg");
            string[] bowesti = { "dijabetes", "sizofrenija" };

            MedicalRecord record = new MedicalRecord(185, 80, bowesti);
            Patient patient = new Patient("Dimitrije", "Gasic", "234567891", DateTime.Now, "06213214", "Trg Dositeja Obradovica 6", "gasara123", "123123", Gender.Male, false, record);
            appointments.Add(new Appointment(patient, doc, new TimeSlot(DateTime.Now, TimeSpan.FromMinutes(15)), false));


            AppointmentsListView.ItemsSource = new ObservableCollection<Appointment>(appointments);

        }

    }
}
