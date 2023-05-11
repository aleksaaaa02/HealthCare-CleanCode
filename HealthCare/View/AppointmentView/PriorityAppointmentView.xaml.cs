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

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
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
            else
            {
                resultAppointment = GetAppointmentByDateAndDoctor(endDate, Int32.Parse(tbHoursStart.Text), Int32.Parse(tbMinutesStart.Text), Int32.Parse(tbHoursEnd.Text), Int32.Parse(tbMinutesEnd.Text), doctor);
                if (resultAppointment == null)
                {
                    resultAppointment = GetAppointmentByDoctor(endDate, Int32.Parse(tbHoursStart.Text), Int32.Parse(tbMinutesStart.Text), Int32.Parse(tbHoursEnd.Text), Int32.Parse(tbMinutesEnd.Text), doctor);
                }
            }

            List<Appointment> appointments = new List<Appointment>();
            appointments.Add(resultAppointment);
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
    }

}
