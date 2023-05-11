using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for PriorityAppointmentView.xaml
    /// </summary>
    public partial class PriorityAppointmentView : Window
    {

        Hospital _hospital;
        public PriorityAppointmentView(Hospital hospital)
        {
            _hospital = hospital;
            InitializeComponent();
            LoadData();
        }


        public void LoadData()
        {
            List<Doctor> doctors = _hospital.DoctorService.GetAll();
            doctorListView.ItemsSource = new ObservableCollection<Doctor>(doctors);
        }

        public bool isValidData()
        {
            Patient patient = (Patient)_hospital.Current;
            if (patient.Blocked)
            {
                Utility.ShowWarning("Zao nam je, ali vas profil je blokiran");
                return false;
            }

            Doctor doctor = (Doctor)doctorListView.SelectedItem;
            int hoursStart = int.Parse(tbHoursStart.Text);
            int minutesStart = int.Parse(tbMinutesStart.Text);

            int hoursEnd = int.Parse(tbHoursEnd.Text);
            int minutesEnd = int.Parse(tbMinutesEnd.Text);
            if (!tbDate.SelectedDate.HasValue)
            {
                Utility.ShowWarning("Molimo Vas izaberite datum");
                return false;
            }
            else
            {
                DateTime currentDate = DateTime.Now;
                DateTime selectedDate = tbDate.SelectedDate.Value;
                selectedDate = selectedDate.AddHours(hoursStart);
                selectedDate = selectedDate.AddMinutes(minutesStart);
                if (selectedDate < currentDate)
                {
                    Utility.ShowWarning("Izaberite ispravan datum pregleda");
                    return false;
                }
            }
            if (doctorListView.SelectedItems.Count != 1)
            {
                Utility.ShowWarning("Molimo Vas izaberite doktora");
                return false;
            }
            if (hoursStart > hoursEnd || (hoursStart == hoursEnd && minutesStart >= minutesEnd))
            {
                Utility.ShowWarning("Molimo Vas izaberite ispravan vremenski interval");
                return false;
            }







            return true;
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            if (!isValidData()) return;
            DateTime endDate = tbDate.SelectedDate.Value;
            Doctor doctor = (Doctor)doctorListView.SelectedItem;
            Appointment resultAppointment;
            if (radioDatum.IsChecked == true)
            {
                resultAppointment = GetAppointmentByDateAndDoctor(endDate, Int32.Parse(tbHoursStart.Text), Int32.Parse(tbMinutesStart.Text), Int32.Parse(tbHoursEnd.Text), Int32.Parse(tbMinutesEnd.Text),doctor);
                if (resultAppointment == null)
                {
                    resultAppointment = GetAppointmentByDate(endDate, Int32.Parse(tbHoursStart.Text), Int32.Parse(tbMinutesStart.Text), Int32.Parse(tbHoursEnd.Text), Int32.Parse(tbMinutesEnd.Text));
                }
            }
            else if(radioDoktor.IsChecked == true)
            {
                resultAppointment = GetAppointmentByDateAndDoctor(endDate, Int32.Parse(tbHoursStart.Text), Int32.Parse(tbMinutesStart.Text), Int32.Parse(tbHoursEnd.Text), Int32.Parse(tbMinutesEnd.Text), doctor);
                if (resultAppointment == null)
                {
                    resultAppointment = GetAppointmentByDoctor(endDate, Int32.Parse(tbHoursStart.Text), Int32.Parse(tbMinutesStart.Text), Int32.Parse(tbHoursEnd.Text), Int32.Parse(tbMinutesEnd.Text), doctor);
                }
            }
            else 
            {
                MessageBox.Show("Izaberite prioritet");
                return; 
            }



            

            List<Appointment> appointments = new List<Appointment>();
            if (resultAppointment == null)
            {
                appointments = GetAppointmentByDoctor(Int32.Parse(tbHoursStart.Text), Int32.Parse(tbMinutesStart.Text), Int32.Parse(tbHoursEnd.Text), Int32.Parse(tbMinutesEnd.Text), doctor);
            }
            else
            {
                appointments.Add(resultAppointment);
            }
            appointmentListView.ItemsSource = new ObservableCollection<Appointment>(appointments);

        }


        Appointment GetAppointmentByDoctor(DateTime endDate, int hoursStart, int minutesStart, int hoursEnd, int minutesEnd, Doctor doctor)
        {
            DateTime startDate = DateTime.Today;
            startDate = startDate.AddMinutes(15);
            Patient patient = (Patient)_hospital.Current;
            while (startDate<endDate)
            {
                TimeSlot timeSlot = new TimeSlot(startDate, new TimeSpan(0, 15, 0));
                if (doctor.IsAvailable(timeSlot) && patient.IsAvailable(timeSlot))
                {
                    return new Appointment(patient, doctor, timeSlot, false);
                }
                else
                {
                    startDate = startDate.AddMinutes(15);
                }
            }
            return null;
        }

        List<Appointment> GetAppointmentByDoctor(int hoursStart, int minutesStart, int hoursEnd, int minutesEnd, Doctor doctor)
        {
            DateTime startDate = DateTime.Today;
            startDate = startDate.AddMinutes(15);
            List<Appointment> appointments = new List<Appointment>();
            Patient patient = (Patient)_hospital.Current;
            while (appointments.Count()<3)
            {
                TimeSlot timeSlot = new TimeSlot(startDate, new TimeSpan(0, 15, 0));
                if (doctor.IsAvailable(timeSlot) && patient.IsAvailable(timeSlot))
                {
                    appointments.Add(new Appointment(patient, doctor, timeSlot, false));
                }
                else
                {
                    startDate = startDate.AddMinutes(15);
                }
            }
            return appointments;
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

        private void tbHoursStart_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = tbHoursStart.Text;
            if (int.TryParse(text, out int hours))
            {
                if (hours > 23 || hours < 0)
                {
                    tbHoursStart.Text = "0";
                }
            }
            else
            {
                tbHoursStart.Text = "0";
            }
        }

        private void tbHoursEnd_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = tbHoursEnd.Text;
            if (int.TryParse(text, out int hours))
            {
                if (hours > 23 || hours < 0)
                {
                    tbHoursEnd.Text = "0";
                }
            }
            else
            {
                tbHoursEnd.Text = "0";
            }
        }

        private void tbMinutesStart_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = tbMinutesStart.Text;
            if (int.TryParse(text, out int minutes))
            {
                if (minutes > 59 || minutes < 0)
                {
                    tbMinutesStart.Text = "0";
                }

            }
            else
            {
                tbMinutesStart.Text = "0";
            }
        }

        private void tbMinutesEnd_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = tbMinutesEnd.Text;
            if (int.TryParse(text, out int minutes))
            {
                if (minutes > 59 || minutes < 0)
                {
                    tbMinutesEnd.Text = "0";
                }

            }
            else
            {
                tbMinutesEnd.Text = "0";
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (doctorListView.SelectedItems.Count != 1) 
            {
                Utility.ShowWarning("Niste izabrali pregled");
                return;
            }
            Appointment appointment = (Appointment)appointmentListView.SelectedItem;
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
                if (updateDeleteCounter >= 5 || createCounter > 8)
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


        public void WriteAction(string action)
        {
            string stringtocsv = _hospital.Current.JMBG + "|" + action + "|" + DateTime.Now.ToShortDateString() + Environment.NewLine;
            File.AppendAllText("../../../Resource/PatientLogs.csv", stringtocsv);
        }
    }

}
