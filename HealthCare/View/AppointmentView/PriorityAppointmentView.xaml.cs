﻿using HealthCare.Context;
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
        PriorityAppointmentViewModel model;
        public PriorityAppointmentView(Hospital hospital)
        {
            _hospital = hospital;
            model = new PriorityAppointmentViewModel(hospital);
            DataContext = model;
            InitializeComponent();
            
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
            int hoursStart = Int32.Parse(tbHoursStart.Text);
            int hoursEnd = Int32.Parse(tbHoursEnd.Text);
            int minutesStart = int.Parse(tbMinutesStart.Text);
            int minutesEnd = int.Parse(tbMinutesEnd.Text);
            if (radioDatum.IsChecked == true)
            {
                model.getAppointments(endDate, hoursStart, minutesStart, hoursEnd, minutesEnd, doctor, "Date");

            }
            else if (radioDoktor.IsChecked == true)
            {
                model.getAppointments(endDate, hoursStart, minutesStart, hoursEnd, minutesEnd, doctor, "Doctor");
            }
            else
            {
                Utility.ShowWarning("Izaberite prioritet");
                return;
            }    
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
            if (appointmentListView.SelectedItems.Count != 1) 
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
            model.IsUserBlocked();
        }

        public void WriteAction(string action)
        {
            string stringtocsv = _hospital.Current.JMBG + "|" + action + "|" + DateTime.Now.ToShortDateString() + Environment.NewLine;
            File.AppendAllText("../../../Resource/PatientLogs.csv", stringtocsv);
        }
    }

}
