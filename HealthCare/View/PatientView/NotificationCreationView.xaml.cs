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

namespace HealthCare.View.PatientView
{
    /// <summary>
    /// Interaction logic for NotificationCreationView.xaml
    /// </summary>
    public partial class NotificationCreationView : Window
    {
        Hospital _hospital;
        PatientNotificationsView _previousView;
        public NotificationCreationView(PatientNotificationsView previousView,Hospital hospital)
        {
            _hospital = hospital;
            _previousView = previousView;
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string caption = txtCaption.Text;
            string text = txtText.Text;
            DateTime selectedDate = datePicker.SelectedDate ?? DateTime.Now.Date;
            int hours, minutes;

            if (string.IsNullOrWhiteSpace(caption) || string.IsNullOrWhiteSpace(text) ||
                !int.TryParse(txtHours.Text, out hours) || !int.TryParse(txtMinutes.Text, out minutes) || datePicker.SelectedDate==null)
            {
                Utility.ShowWarning("Molimo vas unesite sva polja");
                return;
            }

            if (hours < 0 || hours > 23 || minutes < 0 || minutes > 59)
            {
                Utility.ShowWarning("Molimo vas unesite validno vreme");
                return;
            }

            DateTime selectedDateTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, hours, minutes, 0);
            if (selectedDateTime < DateTime.Now)
            {
                Utility.ShowWarning("Datum i vreme koje ste uneli su vec prosli, molimo vas unesite drugi datum i vreme");
                return;
            }
            
            UserNotification notification = new UserNotification(_hospital.Current.JMBG,selectedDateTime, caption, text,  true);
            _hospital.UserNotificationService.Add(notification);
            MessageBox.Show("Uspesno ste dodali notifikaciju");
            _previousView.LoadNotifications();
        }
    }
}
 