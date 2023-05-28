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
            notifications = _hospital.UserNotificationService.GetForUser(_hospital.Current.JMBG);

            foreach (UserNotification userNotification in notifications)
            {
                NotificationControl notificationControl = new NotificationControl();
                notificationControl.NotificationCaption.Text = userNotification.caption;
                notificationControl.NotificationText.Text = userNotification.text;
                if (userNotification.isCustom)
                {
                    SolidColorBrush brush = new SolidColorBrush(Colors.DarkCyan);
                    notificationControl.Header.Background = brush;
                }
                NotificationsPanel.LastChildFill = true;
                DockPanel.SetDock(notificationControl, Dock.Top);
                NotificationsPanel.Children.Add(notificationControl);
            }
        }

        private void expandButton_Click(object sender, RoutedEventArgs e)
        {
            new NotificationCreationView(this,_hospital).Show();
        }
    }
}
