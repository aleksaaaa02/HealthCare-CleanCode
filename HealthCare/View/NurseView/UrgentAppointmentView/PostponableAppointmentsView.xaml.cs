using HealthCare.Context;
using HealthCare.Model;
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
using HealthCare.ViewModel.NurseViewModel;
using HealthCare.Service;

namespace HealthCare.View.UrgentAppointmentView
{
    public partial class PostponableAppointmentsView : Window
    {
        private List<Appointment> appointments = new List<Appointment>();
        private PostponableAppointmentsViewModel vm;
        private Appointment appointment;
        private Appointment newAppointment;
        public PostponableAppointmentsView(Appointment newAppointment, List<Appointment> postponable)
        {
            InitializeComponent();
            this.newAppointment = newAppointment;
            vm = new PostponableAppointmentsViewModel(postponable);
            DataContext = vm;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void lvAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            appointment = (Appointment)lvAppointments.SelectedItem;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (appointment is null) { 
                ShowErrorMessageBox("Izaberite polje iz tabele");
                return;
            }
            newAppointment.TimeSlot.Start = appointment.TimeSlot.Start;
            newAppointment.Doctor = appointment.Doctor;
            Schedule.PostponeAppointment(appointment);
            Schedule.CreateUrgentAppointment(newAppointment);
            Close();
        }

        public void ShowErrorMessageBox(string messageBoxText)
        {
            string content = "Greska";
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBox.Show(messageBoxText, content, button, icon);
        }

    }
}
