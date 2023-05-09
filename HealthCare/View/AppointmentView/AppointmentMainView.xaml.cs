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
            LoadData();
            CheckIfBlock();
        }

        public void WriteAction(string action)
        {
            string stringtocsv = _hospital.Current.JMBG + "|" + action + "|" + DateTime.Now.ToShortDateString() + Environment.NewLine;
            File.AppendAllText("../../../Resource/PatientLogs.csv",stringtocsv);
        }

        Appointment GetAppointmentByDoctor(Doctor doctor)
        {
            DateTime startDate = DateTime.Today;
            startDate = startDate.AddMinutes(15);
            Patient patient = (Patient)_hospital.Current;
            while (true)
            {
                TimeSlot timeSlot = new TimeSlot(startDate,new TimeSpan(0,15,0));       
                if (doctor.IsAvailable(timeSlot) && patient.IsAvailable(timeSlot)) {
                    return new Appointment(patient,doctor,timeSlot,false);
                }
                else
                {
                    startDate = startDate.AddMinutes(15);
                }
            }
        }

        Appointment GetAppointmentByDate(DateTime endDate, int hoursStart, int minutesStart, int hoursEnd, int minutesEnd)
        {
            List<Doctor> doctors = _hospital.DoctorService.GetAll();
            foreach (Doctor doctor in doctors)
            {
                DateTime startDate = DateTime.Today;
                startDate = startDate.AddHours(hoursStart);
                startDate = startDate.AddMinutes(minutesStart);
                Patient patient = (Patient)_hospital.Current;
                while (startDate < endDate)
                {
                    TimeSlot timeSlot = new TimeSlot(startDate, new TimeSpan(0, 15, 0));
                    if (patient.IsAvailable(timeSlot) && doctor.IsAvailable(timeSlot))
                    {
                        return new Appointment(patient, doctor, timeSlot, false);
                    }
                    startDate = startDate.AddMinutes(15);
                    if (startDate.Hour > hoursEnd)
                    {
                        startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day + 1, hoursStart, minutesStart, 0);
                    }
                }
            }
            return null;
        }

        Appointment GetAppointmentByDateAndDoctor(DateTime endDate, int hoursStart, int minutesStart, int hoursEnd, int minutesEnd, Doctor doctor)
        {

            DateTime startDate = DateTime.Today;
            startDate = startDate.AddHours(hoursStart);
            startDate = startDate.AddMinutes(minutesStart);
            Patient patient = (Patient)_hospital.Current;
            while (startDate < endDate)
            {
                TimeSlot timeSlot = new TimeSlot(startDate, new TimeSpan(0, 15, 0));
                if (patient.IsAvailable(timeSlot) && doctor.IsAvailable(timeSlot))
                {
                    return new Appointment(patient, doctor, timeSlot, false);
                }
                startDate = startDate.AddMinutes(15);
                if (startDate.Hour > hoursEnd)
                {
                    startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day + 1, hoursStart, minutesStart, 0);
                }
            }

            return null;
        }



        public void CheckIfBlock()
        {
            Patient patient = (Patient)_hospital.Current;
            using (var reader = new StreamReader("../../../Resource/PatientLogs.csv", Encoding.Default))
            {
                string line;
                int updateDeleteCounter = 0;
                int createCounter = 0;
                while ((line = reader.ReadLine()) != null)
                {

                    string[] values = line.Split('|');
                    if (values[0] == patient.JMBG)
                    {
                        DateTime inputDate = DateTime.Parse(values[2]);
                        DateTime currentDate = DateTime.Now;
                        int daysDifference = (currentDate - inputDate).Days;
                        if (daysDifference < 30)
                        {
                            if (values[1] == "CREATE") createCounter++;
                            if (values[1] == "UPDATE" || values[1] == "DELETE") updateDeleteCounter++;
                        }
                    }


                }
                if(updateDeleteCounter >= 5 || createCounter > 8)
                {
                    patient.Blocked = true;
                }
                else
                {
                    patient.Blocked = false;
                }
                _hospital.PatientService.UpdateAccount(patient);
            }
        }
        public void LoadData()
        {
            List<Appointment> appointments = Schedule.GetPatientAppointments((Patient)_hospital.Current);
            List<Doctor> doctors = _hospital.DoctorService.GetAll();
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
                Utility.ShowWarning("Zao nam je, ali vas profil je blokiran");
                return;
            }
            Doctor doctor = (Doctor)doctorListView.SelectedItem;
            int hours = int.Parse(tbHours.Text);
            int minutes = int.Parse(tbMinutes.Text);
            if (!tbDate.SelectedDate.HasValue)
            {
                Utility.ShowWarning("Molimo Vas izaberite datum");
                return;
            }
            else
            { 
                DateTime currentDate = DateTime.Now;
                DateTime selectedDate = tbDate.SelectedDate.Value;
                selectedDate = selectedDate.AddHours(hours);
                selectedDate = selectedDate.AddMinutes(minutes);
                int difference = (selectedDate - currentDate).Days;
                if (difference < 1)
                {
                    Utility.ShowWarning("Datum pregleda mora biti barem 1 dan od danasnjeg pregleda");
                    return;
                }
            }
            if(doctorListView.SelectedItems.Count != 1)
            {
                Utility.ShowWarning("Molimo Vas izaberite doktora");
                return;
            }
            DateTime date = tbDate.SelectedDate.Value;
            date = date.AddHours(hours);
            date = date.AddMinutes(minutes);
            Appointment appointment = new Appointment(patient, doctor, new TimeSlot(date,new TimeSpan(0,15,0)), false);
            if (!Schedule.CreateAppointment(appointment))
            {
                Utility.ShowWarning("Doktor ili pacijent je zauzet u unetom terminu");
                return;
            }
            Utility.ShowInformation("Uspesno dodat pregled");
            WriteAction("CREATE");
            LoadData();
            CheckIfBlock();
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
                Utility.ShowWarning("Zao nam je, ali vas profil je blokiran");
                return;
            }
            if (appListView.SelectedItems.Count == 1) 
            {
                Appointment appointment = (Appointment)appListView.SelectedItem;
                int idForDeleting = appointment.AppointmentID;
                Schedule.DeleteAppointment(idForDeleting);
                WriteAction("DELETE");
                Utility.ShowInformation("Uspesno obrisan pregled");
                LoadData();
                CheckIfBlock();
            }
            else
            {
                Utility.ShowWarning("Molimo Vas izaberite pregled");
                return;
            }
           
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

            Patient patient = (Patient)_hospital.Current;
            if (patient.Blocked)
            {
                Utility.ShowWarning("Zao nam je, ali vas profil je blokiran");
                return;
            }

            Doctor doctor = (Doctor)doctorListView.SelectedItem;
            int hours = int.Parse(tbHours.Text);
            int minutes = int.Parse(tbMinutes.Text);
            if (!tbDate.SelectedDate.HasValue)
            {
                Utility.ShowWarning("Molimo Vas izaberite datum");
                return;
            }
            else
            {
                DateTime currentDate = DateTime.Now;
                DateTime selectedDate = tbDate.SelectedDate.Value;
                selectedDate = selectedDate.AddHours(hours);
                selectedDate = selectedDate.AddMinutes(minutes);
                int difference = (selectedDate - currentDate).Days;
                if (difference < 1)
                {
                    Utility.ShowWarning("Datum pregleda mora biti barem 1 dan od danasnjeg pregleda");
                    return;
                }
                else
                {

                }
            }
            if (doctorListView.SelectedItems.Count != 1)
            {
                Utility.ShowWarning("Molimo Vas izaberite doktora");
                return;
            }
            DateTime date = tbDate.SelectedDate.Value;
            date = date.AddHours(hours);
            date = date.AddMinutes(minutes);    
            if(appListView.SelectedItems.Count != 1)
            {
                Utility.ShowWarning("Molimo Vas izaberite pregled");
                return;
            }
            Appointment appointment = new Appointment(patient, doctor, new TimeSlot(date, new TimeSpan(0, 15, 0)), false);
            Appointment appointment2 = (Appointment)appListView.SelectedItem;
            appointment.AppointmentID = appointment2.AppointmentID;
            if (!Schedule.UpdateAppointment(appointment))
            {
                Utility.ShowWarning("Doktor ili pacijent je zauzet u unetom terminu");
                return;
            }
            Utility.ShowInformation("Uspesno azuriran pregled");
            WriteAction("UPDATE");
            LoadData();
            CheckIfBlock();

        }
    }
}
