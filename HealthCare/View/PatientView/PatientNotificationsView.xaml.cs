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
    /// Interaction logic for PatientNotificationsView.xaml
    /// </summary>
    public partial class PatientNotificationsView : UserControl
    {
        List<UserNotification> notifications;
        Hospital _hospital;
        public PatientNotificationsView(Hospital hospital)
        {
            _hospital = hospital;
            InitializeComponent();
            LoadNotifications();
            //DataContext = new PatientNotificationsViewModel(hospital);
            
        }

        public void LoadNotifications()
        {
            NotificationsPanel.Children.Clear();
            DateTime currentTime = DateTime.Now;
            Patient patient = (Patient)_hospital.Current;
            int notificationHoursThreshold = patient.NotificationHours;
            notifications = _hospital.UserNotificationService.GetForUser(_hospital.Current.JMBG);
            notifications = notifications.OrderBy(x => x.receiveTime).ToList();
            foreach (UserNotification userNotification in notifications)
            {
                DateTime notificationReceiveTime = userNotification.receiveTime;
                if (notificationReceiveTime > currentTime)
                {
                    TimeSpan timeDifference = notificationReceiveTime - currentTime;

                    if (timeDifference.TotalHours < notificationHoursThreshold)
                    {
                        NotificationControl notificationControl = new NotificationControl();
                        notificationControl.NotificationCaption.Text = userNotification.caption;
                        notificationControl.NotificationText.Text = userNotification.text;
                        if (userNotification.isCustom)
                        {
                            SolidColorBrush brush = new SolidColorBrush(Colors.DarkCyan);
                            notificationControl.Header.Background = brush;
                        }
                        notificationControl.TxtHoursLeft.Text = ((int)timeDifference.TotalHours).ToString() + " sati preostalo";
                        NotificationsPanel.LastChildFill = true;
                        DockPanel.SetDock(notificationControl, Dock.Top);
                        NotificationsPanel.Children.Add(notificationControl);
                    }
                }
            }
        }

        private void expandButton_Click(object sender, RoutedEventArgs e)
        {
            new NotificationCreationView(this,_hospital).Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string hoursText = hoursTextBox.Text;
            if (int.TryParse(hoursText, out int notificationHours))
            {
                if (notificationHours <= 0)
                {
                    Utility.ShowWarning("Broj sati mora biti pozitivan broj!");
                    return;
                }
                else
                {
                    Patient patient = (Patient)_hospital.Current;
                    patient.NotificationHours = notificationHours;
                    _hospital.PatientService.Update(patient);
                    LoadNotifications();
                }
            }
            else
            {
                Utility.ShowWarning("Broj sati mora biti broj");
            }
        }
    }
}
