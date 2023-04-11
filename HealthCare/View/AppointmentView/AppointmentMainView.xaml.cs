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
using HealthCare.Context;
using HealthCare.Command;
using System.IO;

namespace HealthCare.View.AppointmentView
{
    /// <summary>
    /// Interaction logic for AppointmentMainView.xaml
    /// </summary>
    public partial class AppointmentMainView : Window
    {
        Hospital _hospital;
        Appointment selectedAppointment;
        public AppointmentMainView(Hospital hospital)
        {
            InitializeComponent();
            _hospital = hospital;
            loadData();
        }

        public void writeAction(string action)
        {
            string stringtocsv = _hospital.Current.JMBG + "|" + action + "|" + DateTime.Now.ToShortDateString() + Environment.NewLine;
            File.AppendAllText("../../../log/PatientLogs.csv",stringtocsv);
        }
        public void loadData()
        {
            List<Appointment> appointments = Schedule.GetPatientAppointments((Patient)_hospital.Current);
            List<Doctor> doctors = _hospital.DoctorService.Doctors;
            appListView.ItemsSource = new ObservableCollection<Appointment>(appointments);
            doctorListView.ItemsSource = new ObservableCollection<Doctor>(doctors);

        }

        private void tbMinutes_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = tbMinutes.Text;
            if(int.TryParse(text, out int minutes))
            {
                if(minutes > 59 || minutes < 0)
                {
                    tbMinutes.Text = "0";
                }

            }
            else
            {
                tbMinutes.Text = "0";
            }
        }

        private void tbHours_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = tbHours.Text;
            if(int.TryParse(text, out int hours))
            {
                if(hours > 23 || hours < 0)
                {
                    tbHours.Text = "0";
                }
            }
            else
            {
                tbHours.Text = "0";
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)_hospital.Current;
            if (patient.Blocked)
            {
                MessageBox.Show("Zao nam je, ali vas profil je blokiran", "Greska prilikom unosa", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Doctor doctor = (Doctor)doctorListView.SelectedItem;
            int hours = int.Parse(tbHours.Text);
            int minutes = int.Parse(tbMinutes.Text);
            if (!tbDate.SelectedDate.HasValue)
            {
                MessageBox.Show("Molimo Vas izaberite datum", "Greska prilikom unosa", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            { 
                DateTime currentDate = DateTime.Now;
                DateTime selectedDate = tbDate.SelectedDate.Value;
                selectedDate.AddHours(hours);
                selectedDate.AddMinutes(minutes);
                int difference = (selectedDate - currentDate).Days;
                if (difference < 1)
                {
                    MessageBox.Show("Datum pregleda mora biti barem 1 dan od danasnjeg pregleda", "Greska prilikom unosa", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
            if(doctorListView.SelectedItems.Count != 1)
            {
                MessageBox.Show("Molimo Vas izaberite doktora", "Greska prilikom unosa", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            DateTime date = tbDate.SelectedDate.Value;
            date.AddHours(hours);
            date.AddMinutes(minutes);
            Appointment appointment = new Appointment(patient, doctor, new TimeSlot(date,new TimeSpan(0,15,0)), false);
            Schedule.CreateAppointment(appointment);
            MessageBox.Show("Uspesno dodat pregled", "Potvrda", MessageBoxButton.OK, MessageBoxImage.Information);
            writeAction("CREATE");
            loadData();
        }

        private void appListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (appListView.SelectedItems.Count == 1)
            {
                Appointment appointment = (Appointment)appListView.SelectedItem;
                tbDate.SelectedDate = appointment.TimeSlot.Start;
                tbHours.Text = appointment.TimeSlot.Start.Hour.ToString();
                tbMinutes.Text = appointment.TimeSlot.Start.Minute.ToString();
            }
            
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)_hospital.Current;
            if (patient.Blocked)
            {
                MessageBox.Show("Zao nam je, ali vas profil je blokiran", "Greska prilikom brisanja", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (appListView.SelectedItems.Count == 1) 
            {
                Appointment appointment = (Appointment)appListView.SelectedItem;
                int idForDeleting = appointment.AppointmentID;
                Schedule.DeleteAppointment(idForDeleting);
                writeAction("DELETE");
                MessageBox.Show("Uspesno obrisan pregled", "Potvrda", MessageBoxButton.OK, MessageBoxImage.Information);
                loadData();
            }
            else
            {
                MessageBox.Show("Molimo Vas izaberite pregled", "Greska prilikom brisanja", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
           
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

            Patient patient = (Patient)_hospital.Current;
            if (patient.Blocked)
            {
                MessageBox.Show("Zao nam je, ali vas profil je blokiran", "Greska prilikom azuriranja", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Doctor doctor = (Doctor)doctorListView.SelectedItem;
            int hours = int.Parse(tbHours.Text);
            int minutes = int.Parse(tbMinutes.Text);
            if (!tbDate.SelectedDate.HasValue)
            {
                MessageBox.Show("Molimo Vas izaberite datum", "Greska prilikom azuriranja", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                DateTime currentDate = DateTime.Now;
                DateTime selectedDate = tbDate.SelectedDate.Value;
                selectedDate.AddHours(hours);
                selectedDate.AddMinutes(minutes);
                int difference = (selectedDate - currentDate).Days;
                if (difference < 1)
                {
                    MessageBox.Show("Datum pregleda mora biti barem 1 dan od danasnjeg pregleda", "Greska prilikom azuriranja", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {

                }
            }
            if (doctorListView.SelectedItems.Count != 1)
            {
                MessageBox.Show("Molimo Vas izaberite doktora", "Greska prilikom azuriranja", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            DateTime date = tbDate.SelectedDate.Value;
            date.AddHours(hours);
            date.AddMinutes(minutes);    
            if(appListView.SelectedItems.Count != 1)
            {
                MessageBox.Show("Molimo Vas izaberite pregled", "Greska prilikom azuriranja", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Appointment appointment = new Appointment(patient, doctor, new TimeSlot(date, new TimeSpan(0, 15, 0)), false);
            Appointment appointment2 = (Appointment)appListView.SelectedItem;
            appointment.AppointmentID = appointment2.AppointmentID;
            Schedule.UpdateAppointment(appointment);
            MessageBox.Show("Uspesno azuriran pregled", "Potvrda", MessageBoxButton.OK, MessageBoxImage.Information);
            writeAction("UPDATE");
            loadData();

        }
    }
}
