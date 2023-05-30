using HealthCare.Application.Common;
using HealthCare;
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
using HealthCare.Application;
using HealthCare.Service;
using HealthCare.ViewModel.PatientViewModell;

namespace HealthCare.View.PatientView
{
    /// <summary>
    /// Interaction logic for NotificationCreationView.xaml
    /// </summary>
    public partial class NotificationCreationView : Window
    {
        PatientNotificationsViewModel _previousViewModel;
        UserNotificationService _userNotificationService;

        public NotificationCreationView(PatientNotificationsViewModel previousViewModel)
        {
            _previousViewModel = previousViewModel;
            _userNotificationService = Injector.GetService<UserNotificationService>();
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
                ViewUtil.ShowWarning("Molimo vas unesite sva polja");
                return;
            }

            if (hours < 0 || hours > 23 || minutes < 0 || minutes > 59)
            {
                ViewUtil.ShowWarning("Molimo vas unesite validno vreme");
                return;
            }

            DateTime selectedDateTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, hours, minutes, 0);
            if (selectedDateTime < DateTime.Now)
            {
                ViewUtil.ShowWarning("Datum i vreme koje ste uneli su vec prosli, molimo vas unesite drugi datum i vreme");
                return;
            }
            
            UserNotification notification = new UserNotification(Context.Current.JMBG,selectedDateTime, caption, text,  true);
            _userNotificationService.Add(notification);
            MessageBox.Show("Uspesno ste dodali notifikaciju");
            _previousViewModel.LoadNotifications();
        }
    }
}
 